var $j = jQuery.noConflict(true);
$(document).ready(function () {
    LoadData();
    //$(".select option").val(function (idx, val) {
    //    $(this).siblings('[value="' + val + '"]').remove();
    //});
   
});

function LoadData() {
    $.ajax({
        type: "GET",
        url: "/Device/LoadDevice",
        data: '{}',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: OnSuccess,
        failure: function (response) {
            alert(response.d);
        },
        error: function (response) {
            alert(response.d);
        }
    });
}
//Load datatables Device
function OnSuccess(response) {
    $("#tableDevice").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns:
                [

                    { 'data': 'IMEI' },
                    { 'data': 'TOKEN' },
                    { 'data': 'MODEL' },
                    { 'data': 'MAKER' },
                    { 'data': 'BUMON_NAME' },
                    { 'data': 'STORE_NAME' },
                    { 'data': 'INSERT_DATE' },
                    { 'data': 'INSERT_USER' },
                    { 'data': 'UPDATE_USER' },
                    { 'data': 'UPDATE_DATE' },

                    {
                        
                        "render": function (data, type, full, row) {
                            return "<input type='checkbox' id='check' disabled='disabled' onclick=CheckFlag(); />";
                        }
                    },
                    {
                        
                        "render": function (data, type, row, full) {
                            return "<button type='button' id='btnDelete' class='btn btn-danger btnDelete fa fa-trash-o' data-toggle='modal' data-target='#staticBackdropDelete' onclick=DeleteDataDevice('" + row.IMEI + "')  > Delete</button>" + " " +
                            "<button type='button' class='btn btn-success fa fa-pencil-square-o' data-toggle='modal' data-target='#staticBackdropDevice' onclick=getDevice('" + row.IMEI + "')  > Edit</button>";
                        }
                    }

                ],
            columnDefs: [{
                targets: [6, 9], render: function (data) {
                    if (data == null) {
                        return null;

                    }
                    return moment(data).format('YYYY-MM-DD HH:mm:ss');
                }
            }]
        });
}

//Xu ly add Device
function AddDataDevice() {
    
    $("#staticBackdropLabel").text("Add record");
    //Tat thong bao 
    $(".modal-body #imeiID").css('border-color', 'lightgrey');
    $(".modal-body #modelID").css('border-color', 'lightgrey');
    $(".modal-body #makerID").css('border-color', 'lightgrey');
    //Hien nut Save cua chuc nang Add
    $(".modal-footer #submitSaveEdit").hide();
    $(".modal-footer #submitSaveAdd").show();
    $(".modal-body #imeiID").removeAttr('disabled');
    $(".modal-body #imeiID").val("");
    $(".modal-body #modelID").val("");
    $(".modal-body #makerID").val("");
    SortBumonSelect();
    SortStoreSelect();
}

function AddDevice() {
    var res = validate();
    if (res == false) {
        return false;
    }

    var bumon_id = $('.modal-body #selectBumonID :selected').val();
    var store_cd = $('.modal-body #selectStoreCD :selected').val();

    var dvObj = {
        IMEI: $('.modal-body #imeiID').val(),
        MODEL: $('.modal-body #modelID').val(),
        MAKER: $('.modal-body #makerID').val(),
        STORE_CD: store_cd,
        BUMON_ID: bumon_id,
    };
    $.ajax({
        url: "/Device/AddDevice",
        data: JSON.stringify(dvObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //loadData();

            window.location.reload(true);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}

//Edit Device
function getDevice(IMEI) {
  
    $(".modal-body #imeiID").css('border-color', 'lightgrey');
    $(".modal-body #modelID").css('border-color', 'lightgrey');
    $(".modal-body #makerID").css('border-color', 'lightgrey');
    //Khoa input IMEI
    $(".modal-body #imeiID").attr('disabled', 'disabled');
    //Hien nut Save cua chuc nang Edit
    $(".modal-footer #submitSaveAdd").hide();
    $(".modal-footer #submitSaveEdit").show();
   
    $.ajax({
        url: "/Device/getDevice?IMEI=" + IMEI,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $("#staticBackdropLabel").text("Edit Device: " + result.IMEI);
            $(".modal-body #imeiID").val(result.IMEI);
            $(".modal-body #modelID").val(result.MODEL);
            $(".modal-body #makerID").val(result.MAKER);
            $('.modal-body #selectBumonID').val(result.BUMON_ID);
            SortBumonSelect();
            $('.modal-body #selectStoreCD').val(result.STORE_CD);
            SortStoreSelect();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function UpdateDevice() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var bumon_id = $('.modal-body #selectBumonID :selected').val();// lay duoc gia tri
    var store_cd= $('.modal-body #selectStoreCD :selected').val();// lay duoc gia tri
    //alert("bumon id :" + bumon_id + "Store_cd:" + store_cd);
    var obj = {
        IMEI: $(".modal-body #imeiID").val(),
        MODEL: $(".modal-body #modelID").val(),
        MAKER: $(".modal-body #makerID").val(),
        STORE_CD: store_cd,
        BUMON_ID: bumon_id,
    };
    $.ajax({
        url: "/Device/UpdateDevice",
        data: JSON.stringify(obj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //loadData();
            //alert("Update success")
            window.location.reload(true);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });

   
        
    
}

//Xu ly Delete Device
function DeleteDataDevice(IMEI) {
    $("#submitDeleteDevice").click(function () {
        $.ajax({
            url: "/Device/DeleteDevice?IMEI=" + IMEI,
            type: "POST",
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                //loadData();
                window.location.reload(true);
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });  
    });
};

//Kiem tra rang buoc CHO NHAP imei, model,maker
function validate() {
    var isValid = true;
    var fn = $("#imeiID").val();
    var regex = /^[0-9a-zA-Z\_]+$/;
    if ($('#imeiID').val().trim() == "") {
        $('#imeiID').css('border-color', 'Red');
        isValid = false;
    } else if (regex.test(fn) == false) {
        $('#imeiID').css('border-color', 'Red');
        isValid = false;
        alert("Do not have space in this text");
    } else {
        $('#imeiID').css('border-color', 'lightgrey');
    }
    if ($('#modelID').val().trim() == "") {
        $('#modelID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#modelID').css('border-color', 'lightgrey');
    }
    if ($('#makerID').val().trim() == "") {
        $('#makerID').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#makerID').css('border-color', 'lightgrey');
    }


    return isValid;
}

// Sap xep select Bumon
function SortBumonSelect() {
    var options = $(".modal-body #selectBumonID option");
    options.detach().sort(function (a, b) {
        var at = $(a).text();
        var bt = $(b).text();
        return (at > bt) ? 1 : ((at < bt) ? -1 : 0);
    });
    options.appendTo(".modal-body #selectBumonID");

}

//Sap xep select Store
function SortStoreSelect() {
    var options = $(".modal-body #selectStoreCD option");
    options.detach().sort(function (a, b) {
        var at = $(a).text();
        var bt = $(b).text();
        return (at > bt) ? 1 : ((at < bt) ? -1 : 0);
    });
    options.appendTo(".modal-body #selectStoreCD");
}

// Kiem tra check box de thuc hien chuc nang delete
function CheckFlag() { //DANG CHINH
    //if (DEL_FLAG == 0) {
    //    $("#btnDelete").removeAttr('disabled');
    //}
    //alert();
   // if (DEL_FLAG == 0) {
    if ($("#check:checkbox:checked").length > 0) {
            $("#btnDelete").attr("disabled", true);
            
        } else {
            $("#btnDelete").removeAttr('disabled');
        }
   //}
        
    
    
}

