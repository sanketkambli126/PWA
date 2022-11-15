var dtOrderLines, supplierOrderId;
import { BackButton } from "../common/utilities.js"
import { ProcurementViewQuotePageKey } from "../common/constants.js"
$(document).ready(function () {
    BackButton(ProcurementViewQuotePageKey, false);
    var supplierDetailsHeight = $("#cdSupplierDetails").height();
    var quoteDetailsHeight = $("#cdQuoteDetails").height();
    var maxHeight = Math.max(quoteDetailsHeight, supplierDetailsHeight);

    $(window).resize(function () {

        if (($(this).width() >= 992) && (supplierDetailsHeight != quoteDetailsHeight)) {
            $("#cdSupplierDetails").height(maxHeight);
            $("#cdQuoteDetails").height(maxHeight);
        }
    });

    if (($(this).width() >= 992) && (supplierDetailsHeight != quoteDetailsHeight)) {
        $("#cdSupplierDetails").height(maxHeight);
        $("#cdQuoteDetails").height(maxHeight);
    }

    $('.height-equal').matchHeight();

    supplierOrderId = $('#hdnSupplierOrderId').val();

    $('#dtOrderLines').DataTable().destroy();
    dtOrderLines = $('#dtOrderLines').DataTable({
        "dom": '<<"row"<"col-12 col-md-7"i><"col-12 col-md-5"f>><rt><"row"<"col-12 col-md-7"l><"col-12 col-md-5"p>>>',
        "processing": true,
        "serverSide": false,
        "lengthChange": true,
        "searching": true,
        "info": true,
        "autoWidth": false,
        "paging": true,
        "pageLength": 25,
        "order": [],
        "language": {
            "emptyTable": "No details available.",
        },
        "initComplete": function (settings, json) {
            if (dtOrderLines.data().count()) {
                document.getElementById('cdCostDetails').style.display = "none";

                var filteredJSON = json.data.filter(x => (x.qtyEnq > 0) && (x.unitPrice == 0));

                //No
                if (filteredJSON.length > 0) {
                    $('#spIsAllItemsQuoted').text('No');
                    $('#spIsAllItemsQuoted').addClass('text-danger');
                }
                //Yes
                else {
                    $('#spIsAllItemsQuoted').text('Yes');
                    $('#spIsAllItemsQuoted').addClass('text-success');
                }
            }
            else {

                if ($('#IsCompleteQuote').val() == 'Yes') {
                    $('#spIsAllItemsQuoted').text('Yes');
                    $('#spIsAllItemsQuoted').addClass('text-success');
                }
                else if ($('#IsCompleteQuote').val() == 'No') {
                    $('#spIsAllItemsQuoted').text('No');
                    $('#spIsAllItemsQuoted').addClass('text-danger');
                }
                document.getElementById('divAllItemsQuoted').style.display = "none";
                document.getElementById('cdLineDetails').style.display = "none";
            }
        },
        "ajax": {
            "url": "/PurchaseOrder/PostGetSupplierQuoteOrderLines",
            "type": "POST",
            "data": {
                "supplierOrderId": supplierOrderId
            },
            "datatype": "json"
        },

        "columns": [
            {
                className: "d-none d-sm-table-cell text-left",
                width: "20px",
                data: "itemNo",
                type: "html-num",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetCellData('#', full.itemNo);
                    }
                    return data;
                }
            },
            {
                className: "tdblock td-row-header",
                data: "partName",
                orderable: false,
                render: function (data, type, full, meta) {
                    return '<span class="d-inline-block d-sm-none">' + full.itemNo + '&nbsp;' + '</span>' + full.partName;
                }
            },
            {
                className: "tdblock",
                name: "MakersReference",
                data: "makersReference",
                render: function (data, type, full, meta) {
                    return GetCellData('Makers Reference', full.makersReference);
                }
            },
            {
                className: "text-right td-w-32",
                name: "QtyEnq",
                data: "qtyEnq",
                type: "html-num",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        if (data != null || data != undefined || data != "") {
                            return GetCellData('Qty Enq', full.qtyEnq);
                        }
                        else {
                            return GetCellData('Qty Enq', '0');
                        }
                    }
                    return data;
                }
            },
            {
                className: "text-right td-w-32",
                name: "SupplierQty",
                data: "supplierQty",
                type: "html-num",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        if (full.isSupplierQtyMismatch == true) {
                            return GetCellData('Supplier Qty', '<span class="text-danger">' + full.supplierQty + '</span>');
                        }
                        return GetCellData('Supplier Qty', full.supplierQty);
                    }
                    return data;
                }
            },
            {
                className: "td-w-18",
                name: "UOM",
                data: "uom",
                width: "37px",
                render: function (data, type, full, meta) {
                    return GetCellData('UOM', full.uom);
                }
            },
            {
                className: "text-right td-w-auto td-min-w-32",
                name: "UnitPrice",
                data: "unitPrice",
                type: "html-num",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        var text = NumberToString(data, 2, 1);
                        return GetCellData('Unit Price', text);
                    }
                    return data;
                }
            },
            {
                className: "text-right td-w-auto",
                name: "DiscountPercent",
                data: "discountPercent",
                width: "60px",
                type: "html-num",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetCellData('Discount (%)', full.discountPercent);
                    }
                    return data;
                }
            },
            {
                className: "text-right td-w-auto",
                name: "SubTotal",
                data: "subTotal",
                type: "html-num",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetCellData('Sub Total', full.subTotal);
                    }
                    return data;
                }
            },
            {
                className: "text-right td-w-auto",
                name: "ExDays",
                data: "exDays",
                width: "35px",
                type: "html-num",
                render: function (data, type, full, meta) {
                    if (type === "display") {
                        return GetCellData('Ex Days', full.exDays);
                    }
                    return data;
                }
            },
            {
                className: "tdblock",
                name: "Notes",
                data: "notes",
                render: function (data, type, full, meta) {
                    return GetCellData('Notes', full.notes);
                }
            },
        ],
    });

});

function NumberToString(number, toFixedValue, state) {
    var numToString = "";
    if (number != null && number != '' && number != 'undefined') {
        numToString = Number(parseFloat(number).toFixed(toFixedValue)).toLocaleString('en',
            {
                minimumFractionDigits: 2
            });
    }
    else {
        if (state == 1) {
            //when expected return is 0.00
            numToString = "0.00";
        }
        else if (state == 2) {
            //when blank space is expected
            numToString = "";
        }
    }
    return numToString;
}

function GetCellData(label, data) {
    return '<label>' + label + '</label><br />' + data;
}

function getCookie(name) {
    var cookieArr = document.cookie.split(";");

    for (var i = 0; i < cookieArr.length; i++) {
        var cookiePair = cookieArr[i].split("=");

        if (name == cookiePair[0].trim()) {
            return decodeURIComponent(cookiePair[1]);
        }
    }
    return null;
}

function tosterAlert(type, message) {
    if (type == "success") {

        toastr.success(message);
    }
    else if (type == "error") {
        toastr.options = {
            "closeButton": true,
            "timeOut": "0",
            "extendedTimeOut": "0"
        };
        toastr.error(message);
    }
}