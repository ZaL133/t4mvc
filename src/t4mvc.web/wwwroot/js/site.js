﻿$(function () {
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
            var self = this;
            var excelButtonHtml = '<button><img src="/content/img/Excel.gif" style="max-height:20px;"> Export</button>',
                previewButtonHtml = '<button><img src="/content/img/pdf.png" style="max-height:20px;"> Preview</button>';

            var tableElement = this.api().table().container();

            // format excel button
            var excelButton = $(tableElement).find(".excel-dt-button");

            if (excelButton) {
                excelButton.html(excelButtonHtml);
                excelButton.click(function (e) {
                    e.preventDefault();
                    var dt = $(this).siblings(".dataTable").DataTable();
                    var originalUrl = dt.ajax.url();
                    var exportUrl = originalUrl.replace("/api/", "/api/export/");

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
            if (previewButton) {
                previewButton.html(previewButtonHtml);
                previewButton.click(function (e) {
                    e.preventDefault();

                    var dt = $(this).siblings(".dataTable").DataTable();
                    var originalUrl = dt.ajax.url();

                    // This means this is an ajax request
                    if (originalUrl) {
                        var exportUrl = originalUrl.replace("/api/", "/exportpdf/");

                        window.open(exportUrl);
                    } else { // If not, this is a client side data table.
                        generateContentToNewWindow("/exportpdf/generatepreview", "/exportpdf/preview");
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
                    url: postUrl,
                    method: "POST",
                    dataType: "JSON",
                    contentType: "application/json",
                    data: JSON.stringify({
                        columns: columns, data: data, columnDefs: columnDefs
                    }),
                    success: function (result) {
                        if (result.keyid) {
                            var exportFile = $(tableElement).find("[data-export-file]");
                            var fileName = (exportFile && exportFile.length > 0) ? exportFile.attr("data-export-file").replace(/[/\\?%*:|"<>]/g, '-') : "results.xlsx";
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
        },
    });

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

    // Data table - default options
    $(".t4mvc-data-table").dataTable();
});

t4mvc = (function () {
    var publicApi               = { api: {} },
        tzOffset                = new Date().getTimezoneOffset(),
        userSessionStorageKey   = "t4mvc-USERS";

    function navigateUpOneLevel() {
        var parts = window.location.href.split("/");
        parts.pop();

        if (/(edit|details|create)/i.test(parts[parts.length - 1])) parts.pop();
        window.location.href = parts.join("/");
    }


    publicApi.clientSideExcelButtonDom = 'frl<"cs-excel-dt-button"><"preview-dt-button">tip';
    publicApi.excelButtonDom = 'frl<"excel-dt-button"><"preview-dt-button">tip';
    publicApi.navigateUpOneLevel = navigateUpOneLevel;

    return publicApi;
})();