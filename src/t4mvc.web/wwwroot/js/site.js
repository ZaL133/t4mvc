$(function () {
    function getFunctionReference(r) {
        if (r.indexOf(".") > -1) {
            var parts = r.split(".");
            return window[parts[0]][parts[1]];
        } else {
            return window[r];
        }
    }

    // datatables config
    $.extend(true, $.fn.dataTable.defaults, {
        initComplete: function () {
            var self                = this;
            var excelButtonHtml     = '<button><i data-feather="download" style="max-height:20px;"></i> Export</button>',
                previewButtonHtml   = '<button><i data-feather="search" style="max-height:20px;"></i> Preview</button>';

            var tableElement = this.api().table().container();

            // format excel button
            var excelButton = $(tableElement).find(".excel-dt-button");

            if (excelButton) {
                excelButton.html(excelButtonHtml);
                excelButton.click(function (e) {
                    e.preventDefault();
                    var dt          = $(this).siblings(".dataTable").DataTable();
                    var originalUrl = dt.ajax.url();
                    var exportUrl   = originalUrl.replace("/api/", "/api/export/");

                    window.open(exportUrl);
                });
            }

            var csExcelButton = $(tableElement).find(".cs-excel-dt-button");
            if (csExcelButton) {
                csExcelButton.html(excelButtonHtml);

                csExcelButton.click(function (e) {
                    e.preventDefault();

                    generateContentToNewWindow("/api/export/datatable", "/api/export/downloaddatatable");
                });
            }

            var previewButton = $(tableElement).find(".preview-dt-button");
            if (previewButton)
            {
                previewButton.html(previewButtonHtml);
                previewButton.click(function (e) {
                    e.preventDefault();

                    var dt = $(this).siblings(".dataTable").DataTable();
                    var originalUrl = dt.ajax.url();

                    // This means this is an ajax request
                    if (originalUrl) {
                        var exportUrl = originalUrl.replace("/api/", "/report/");

                        window.open(exportUrl);
                    } else { // If not, this is a client side data table.
                        generateContentToNewWindow("/report/generatepreview", "/report/preview");
                    }
                });
            }

            function generateContentToNewWindow(postUrl, getUrl) {
                t4mvc.startSpinner();

                var table = self.api().table();

                var columns = table.columns().header().map(function (val, ix) {
                    return val.innerText;
                }).toArray();

                var data = table.rows({ filter: 'applied' }).data().toArray();

                // Get the formatting function name
                var columnDefs = table.settings()[0].aoColumns.map(function (x) {
                    return x.mRender ? x.mRender.name : null;
                });

                $.ajax({
                    url:        postUrl,
                    method:     "POST",
                    dataType:   "JSON",
                    contentType: "application/json",
                    data: JSON.stringify({
                        columns: columns, data: data, columnDefs: columnDefs
                    }),
                    success: function (result) {
                        if (result.keyid) {
                            var exportFile  = $(tableElement).find("[data-export-file]");
                            var fileName    = (exportFile && exportFile.length > 0) ? exportFile.attr("data-export-file").replace(/[/\\?%*:|"<>]/g, '-') : "results.xlsx";
                            window.open(getUrl + "?id=" + result.keyid + "&fileName=" + fileName, "_blank");
                            t4mvc.stopSpinner();
                        }
                        else {
                            t4mvc.stopSpinner();
                            toastr.error("Unable to download file");
                        }
                    },
                    error: function (err) {
                        t4mvc.stopSpinner();
                        toastr.error("Error downloading file.");
                    }
                });
            }

            // Fix the icons
            feather.replace();
        },
    });

    // jQuery config
    $.ajaxSetup({
        cache: false,
        // Handle the request verification token
        beforeSend: function (request) {
            var reqVerToken = $("[name='__RequestVerificationToken']").val();
            if (reqVerToken)
                request.setRequestHeader("RequestVerificationToken", reqVerToken);
        }
    });

    // Client side data table needs some custom configuration
    $(".clientSideExcelButtonDataTable").each(function (ix, elem) {
        var id = elem.id;
        var columnDefs = [];
        if (window.hasOwnProperty("renderFuncs")) {
            var funcList = window.renderFuncs[id];
            if (funcList != null) {
                for (var i = 0; i < funcList.length; i++) {
                    funcList[i].render = getFunctionReference(funcList[i].render);

                }
                columnDefs = funcList;
            }
        }
        $(elem).DataTable({
            dom: t4mvc.clientSideExcelButtonDom,
            columnDefs: columnDefs
        });
    })

    // Initialize Select2 fields 
    // Select2 Setup
    var select2s = $(".t4mvc-select2");
    select2s.each(function (i, e) {
        var $e = $(e);
        var t4mvcApi = $e.attr("data-t4mvc-api"),
            t4mvcId = $e.attr("data-t4mvc-id") || "id",
            t4mvcText = $e.attr("data-t4mvc-text") || "text",
            t4mvcPrefetch = $e.attr("data-t4mvc-prefetch") == "True" || false,
            process = function (data, t4mvcId, t4mvcText) {
                return $.map(data, function (item) {
                    return { id: (t4mvcId ? item[t4mvcId] : item), text: (t4mvcText ? item[t4mvcText] : item) }
                })
            },
            // get the function for processing results. Default to the above, but allow override
            t4mvcProcess = $e.attr("data-t4mvc-process") ? function (data) {
                return t4mvc[$e.attr("data-t4mvc-process")](data, t4mvcId, t4mvcText);
            } : function (data) { return process(data, t4mvcId, t4mvcText); },
            t4mvcAttachTo = $("#" + $e.attr("data-t4mvc-attach"));

        var defaultSelectOptions = { placeholder: "Please select", selectOnClose: true };
        if (t4mvcAttachTo.length) defaultSelectOptions["dropdownParent"] = t4mvcAttachTo;

        if (t4mvcPrefetch) {
            $.ajax({
                method: "GET",
                url: t4mvcApi,
                success: function (data) {
                    var items = t4mvcProcess(data);
                    defaultSelectOptions["data"] = items;
                    $e.select2(defaultSelectOptions);
                }
            })
        }
        else {
            defaultSelectOptions["ajax"] = {
                dataType: "json",
                delay: 100,
                width: "100%",
                method: "GET",
                data: function (params) {
                    return {
                        query: params.term
                    };
                },
                url: t4mvcApi,
                processResults: function (data) {

                    var processedData = t4mvcProcess(data);

                    return { "results": processedData };
                }
            };
            defaultSelectOptions["minimumInputLength"] = 1;
            $e.select2(defaultSelectOptions);
        }
    });

    // Data table - default options
    $(".t4mvc-data-table").dataTable();

    // Formatters 
    $("[data-render-function]").each(formatRenderFunction);
    function formatRenderFunction(i, elem) {
        var $elem = $(elem);
        var $field = $elem.find("input[disabled]");

        if ($field && $field.length > 0) {
            var funcName = $elem.attr("data-render-function");
            var func = getFunctionReference(funcName);
            $field[0].type = "text";
            $field.val(func($field.val()));
        }
    }

    // User thumbnail
    $("[data-user-thumbnail]").each(function () {
        var $this = $($(this)[0]), userId = $this.attr("data-user-thumbnail");
        if (userId) {
            var userRecord = t4mvc.getUserRecord(userId);

            if (userRecord) {
                if (userRecord && userRecord.ProfilePic) {
                    $this.attr("src", userRecord.ProfilePic);
                    $this.show();
                }
            } else {
                setTimeout(function () {
                    var userRecord = t4mvc.getUserRecord(userId);
                    if (userRecord && userRecord.ProfilePic) {
                        $this.attr("src", userRecord.ProfilePic);
                        $this.show();
                    }
                }, 400);
            }
        }
    })

    // User fullname
    $("[data-user-fullname]").each(function () {
        var $this = $($(this)[0]), userId = $this.attr("data-user-fullname");
        if (userId) {
            var userRecord = t4mvc.getUserRecord(userId);

            if (userRecord) {
                if (userRecord && userRecord.FullName) {
                    $this.text(userRecord.FullName);
                }
            } else {
                setTimeout(function () {
                    var userRecord = t4mvc.getUserRecord(userId);
                    if (userRecord && userRecord.FullName) {
                        $this.text(userRecord.FullName || "(user not found)");
                    }
                }, 400);
            }
        }
    })

    // Setup feather
    feather.replace();

    // Setup wysiwyg editors 
    $("[data-t4mvc-wysiwyg]").summernote({ height: 300, tabsize: 2 });

    // Key bindings
    // Global key bindings
    (function () {
        $("a[data-key-combo]").each(function (ix, elem) {
            var keyCombo = elem.getAttribute("data-key-combo");

            if (keyCombo) {
                t4mvc.setNonGlobalSearchKeyBinding(keyCombo, function () {
                    elem.click();
                });
            }
        });

        key("ctrl+s", function () {
            t4mvc.unbindNonGlobalSearch();
            $("#global-search").focus();
            return false;
        });
        key("esc", t4mvc.escapeButtonPressed);
        key("ctrl+e", function () {
            var editButton = $("#edit-button");
            if (editButton && editButton[0] && editButton[0].tagName == "A") {
                window.location.href = editButton.attr("href");
                return false;
            }
        });
        //key('c', function () {
        //    window.location.href = "/servicedesk/ticket/create";
        //    return false;
        //});
    })();
});

t4mvc = (function () {
    const publicApi                 = { api: {} },
        tzOffset                    = new Date().getTimezoneOffset(),
        userSessionStorageKey       = "t4mvc-USERS",
        nonGlobalSearchKeyBindings  = [];

    // If you press the escape button, it should cancel out of edit mode if you're editing a record. If not, it should take you back
    // Actually it should clik the back button
    function escapeButtonPressed() {
        if (/\/edit\//i.test(window.location.href))
            window.location.href = window.location.href.replace(/\/edit\//i, "/details/");
        else
            navigateUpOneLevel();
    }

    // Formats a string as a phone number
    function formatPhoneNumber(phone) {
        // Null or empty
        if (!phone) return phone;
        var tempPhone       = phone;
        var tempExt         = '';
        var tempTrunkPrefix = "";
        //Look for trunk code prefix (leading 1)
        if (tempPhone.substring(0, 1) === "1") {
            tempTrunkPrefix     = tempPhone.substring(0, 1);
            tempPhone           = tempPhone.substring(1, tempPhone.length);
        }
        //Look for extension
        var extPos = tempPhone.toUpperCase().indexOf('X');
        if (extPos > -1) {
            //If extension found, extract to its own var
            tempExt     = tempPhone.substring(extPos);
            tempPhone   = tempPhone.substring(0, extPos);
        }
        //Check for extension with no "x"
        if (tempPhone.length > 10 && extPos < 0) {
            tempExt     = 'x' + tempPhone.substring(10, tempPhone.length);
            tempPhone   = tempPhone.substring(0, 10);
        }
        //Format phone number based on length. If it's not one of the
        //2 recognized lengths, don't format it.
        if (tempPhone.length === 10) {
            phone = '(' + tempPhone.substr(0, 3) + ') ' + tempPhone.substr(3, 3) + '-' + tempPhone.substr(6, 4);
        } else if (tempPhone.length === 7) {
            phone = tempPhone.substr(1, 3) + '-' + tempPhone.substr(3, 4);
        } else phone = tempPhone;

        //Add extension and trunk code prefix back
        phone = tempTrunkPrefix + " " + phone + " " + tempExt;
        return phone;
    }

    // Get user record
    function getUserRecord(userId) {
        var userList = sessionStorage.getItem(userSessionStorageKey);
        var userObje = JSON.parse(userList);

        if (userObje) return userObje.filter(function (e) { return e.UserId === userId; })[0];
        else return null;
    }

    function navigateUpOneLevel() {
        var parts = window.location.href.split("/");
        parts.pop();

        if (/(edit|details|create)/i.test(parts[parts.length - 1])) parts.pop();
        window.location.href = parts.join("/");
    }

    // Register a custom key binding
    function setNonGlobalSearchKeyBinding(keyCombo, fn) {
        nonGlobalSearchKeyBindings.push(keyCombo);
        key(keyCombo, fn);
    }

    // This removes all key bindings. This is necessary when entering a text area so that normal editing can take place without triggering key combo's
    function unbindNonGlobalSearch() {
        if (nonGlobalSearchKeyBindings && nonGlobalSearchKeyBindings.length > 0) {
            for (var i = 0; i < nonGlobalSearchKeyBindings.length; i++) {
                key.unbind(nonGlobalSearchKeyBindings[i]);
            }
        }
    }

    // Initialize the users object
    // This is cached per session
    // Doesn't work if you're not logged in though so test for the login page
    if (!sessionStorage.getItem(userSessionStorageKey) && !/identity\/account\/login/i.test(window.location.pathname)) {
        $.ajax({
            url: "/api/all-users",
            success: function (userList) {
                var usersJson = JSON.stringify(userList);
                sessionStorage.setItem(userSessionStorageKey, usersJson);
            },
            error: function (errorMessage) { console.log(errorMessage); }
        });
    }

    publicApi.clientSideExcelButtonDom  = 'frl<"cs-excel-dt-button"><"preview-dt-button">tip';
    publicApi.excelButtonDom            = 'frl<"excel-dt-button"><"preview-dt-button">tip';

    publicApi.escapeButtonPressed       = escapeButtonPressed;
    publicApi.formatPhoneNumber         = formatPhoneNumber;
    publicApi.getUserRecord             = getUserRecord;
    publicApi.navigateUpOneLevel        = navigateUpOneLevel;
    publicApi.setNonGlobalSearchKeyBinding = setNonGlobalSearchKeyBinding;
    publicApi.unbindNonGlobalSearch     = unbindNonGlobalSearch;

    return publicApi;
})();