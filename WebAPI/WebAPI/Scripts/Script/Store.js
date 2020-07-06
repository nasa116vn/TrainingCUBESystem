

//Lay du lieu len datatables
var $j = jQuery.noConflict(true);
$(document).ready(function () {
    LoadData();
    //    //$("#tableStore1").DataTable();
    //    //DeleteData();
    //    //EditData();
    //    //AddStore();

});
function LoadData() {
    $.ajax({
        type: "GET",
        url: "/Store/LoadStore",
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
};

//Load datatabls Store
function OnSuccess(response) {
   $("#tableStore").DataTable(
        {
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns:
                [

                    { 'data': 'STORE_CD' },
                    { 'data': 'STORE_NAME' },
                    { 'data': 'INSERT_USER' },
                    { 'data': 'INSERT_DATE' },
                    { 'data': 'UPDATE_USER' },
                    { 'data': 'UPDATE_DATE' },
                    {
                        "render": function (data, type, row,full) {
                            return "<input type='checkbox' disabled='disabled' class='checkID' id='check' onclick=CheckFlag('" + row.STORE_CD + "'); />";
                        }
                    },
                    {
                        "render": function (data, type, row, full) {
                            //return "<a href='#' class='btn btn-danger'  onclick=DeleteData('" + row.STORE_CD + "'); >Delete</a>" + "<a href='#' class='btn btn-success' onclick=ShowModalEdit('" + row.STORE_CD + "');>Edit</a>";
                            return "<button type='button' id='btnDelete' class='btn btn-danger btnDelete fa fa-trash-o' data-toggle='modal' data-target='#staticBackdropDelete' onclick=DeleteData('" + row.STORE_CD + "');  > Delete</button>" + "   " +
                                "<button type='button' class='btn btn-success fa fa-pencil-square-o' data-toggle='modal' data-target='#staticBackdropStore' onclick=getStore('" + row.STORE_CD + "'); > Edit </button>";
                        }
                    }

                ],
            columnDefs:
                [
                    
                   {
                        targets: [3, 5], render: function (data) {
                            if (data == null) {
                                return null;
                            }
                            return moment(data).format('YYYY-MM-DD HH:mm:ss');
                        }
                        
                   }
                   
               ]


        });

};


//Add Store
function AddData() {
    //Tat thong bao
    $('.modal-body #STORE_CD').css('border-color', 'lightgrey');
    $('.modal-body #STORE_NAME').css('border-color', 'lightgrey');
    //Mo khoa Store_cd
    $('.modal-body #STORE_CD').removeAttr('disabled');

    $('#staticBackdropLabel').text("Add store");
    $(".modal-body #STORE_CD").val("");
    $(".modal-body #STORE_NAME").val("");
    //Hien nut Save cua chuc nang Edit
    $(".modal-footer #submitSaveEdit").hide();
    $(".modal-footer #submitSaveAdd").show();

   
    //var inputStore_name = $(".modal-body #STORE_NAME");
    //inputStore_name.addEventListener("keyup", function (event) {
    //    if (event.keyCode === 13) {
    //        event.preventDefault();

    //        $(".modal-footer #submitSaveAdd").click();
    //    }
    //});
}



function AddStore() {
        
        var res = validate();
        if (res == false) {
            return false;
        }
       
        var stoObj = {
            STORE_CD: $('#STORE_CD').val(),
            STORE_NAME: $('#STORE_NAME').val(),

        };
        $.ajax({
            url: "/Store/Add",
            data: JSON.stringify(stoObj),
            type: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            success: function (result) {
                //loadData();
               
                window.location.reload(true);
            },
            error: function (errormessage) {
                alert("Cant add new Store");
            }
        });
}

//Edit Store
function getStore(STORE_CD) {
    //Tat thong bao
    $('.modal-body #STORE_CD').css('border-color', 'lightgrey');
    $('.modal-body #STORE_NAME').css('border-color', 'lightgrey');
    // Khoa Store_cdkhi edit
    $('.modal-body #STORE_CD').attr('disabled', 'disabled');

    //Hien nut Save cua chuc nang Edit
    $(".modal-footer #submitSaveAdd").hide();
    $(".modal-footer #submitSaveEdit").show();

    $.ajax({
        url: "/Store/GetStore?STORE_CD=" + STORE_CD,
        typr: "GET",
        contentType: "application/json;charset=UTF-8",
        dataType: "json",
        success: function (result) {
            $('#staticBackdropLabel').text("Edit Store cd: " + result.STORE_CD);
            $('.modal-body #STORE_CD').val(result.STORE_CD);
            $('.modal-body #STORE_NAME').val(result.STORE_NAME);

        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
    return false;
}

function Update() {
    var res = validate();
    if (res == false) {
        return false;
    }
    var storeObj = {
        STORE_CD: $('#STORE_CD').val(),
        STORE_NAME: $('#STORE_NAME').val(),

    };
    $.ajax({
        url: "/Store/UpdateStore",
        data: JSON.stringify(storeObj),
        type: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        success: function (result) {
            //alert("update success")
            window.location.reload(true);
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}  

//Xoa Store
function DeleteData(STORE_CD) {

    $(".modal-footer #submitDelete").click(function () {    
       $.ajax({  
            url: "/Store/DeleteStore?STORE_CD=" + STORE_CD,  
            type: "POST",  
            contentType: "application/json;charset=UTF-8",  
            dataType: "json",  
            success: function (result) {  
                //table.ajax.reload(true);
                //$("#thongBao").addClass("fade").modal("show");
                //$(".modal-footer #submitSuccess").click(function () {
                    window.location.reload(true);
                //});
               
                
                
            },  
            error: function (errormessage) {  
                alert(errormessage.responseText);  
            }  
        });  
    });
}


//Kiem tra khong nhap chuoi rong cho chuc nang add
function validate() {
    var isValid = true;
    var fn = $("#STORE_CD").val();
    var regex = /^[0-9a-zA-Z\_]+$/
    if ($('#STORE_CD').val().trim() == "") {
        $('#STORE_CD').css('border-color', 'Red');
        isValid = false;
    }
    else if (regex.test(fn) == false) {
        $('#STORE_CD').css('border-color', 'Red');
        isValid = false;
        alert("Do not have space in this text");
    } else {
        $('#STORE_CD').css('border-color', 'lightgrey');
    }
    if ($('#STORE_NAME').val().trim() == "") {
        $('#STORE_NAME').css('border-color', 'Red');
        isValid = false;
    }
    else {
        $('#STORE_NAME').css('border-color', 'lightgrey');
    }
    

    return isValid;
}

//Kiem tra check cho chuc nang delete //Chi moi lam duoc dong dau tien
function CheckFlag(STORE_CD) { //DANG CHINH

    //alert(STORE_CD);

    if ($(".check:checkbox:checked").length > 0) {
        $("#btnDelete").attr("disabled", true);

    } else {
        $("#btnDelete").removeAttr('disabled');
    }

  
}

//function showDialogThongBao() {
//    //$("#staticBackdropDelete").removeClass("fade").modal("hide");
//    $("#thongBao").addClass("fade").modal("show");
//}




