

$(document).ready(function () {
    LoadData();



});


//LoadData
function LoadData () {
    $.ajax({
        type: "POST",
        url: "/View/LoadView",
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
//Load du lieu View len datatable
function OnSuccess(response) {
   var table =  $("#tableView").DataTable(
        {
            //"oLanguage": {
            //    "sSearch": "Filter Data" // Doi ten chu search
            //},
            //"iDisplayLength": -1,
            //"sPaginationType": "full_numbers",
            //dom: 'lrtip', //dung de xoa thanh search
            bLengthChange: true,
            lengthMenu: [[5, 10, -1], [5, 10, "All"]],
            bFilter: true,
            bSort: true,
            bPaginate: true,
            data: response,
            columns:
                [

                    { 'data': 'IMEI' },
                    { 'data': 'BUMON_NAME' },
                    { 'data': 'STORE_NAME' },
                    { 'data': 'PRODUCT_NAME' },
                    { 'data': 'VIEW_DATE' },
                    { 'data': 'VIEWS' },
                    { 'data': 'INSERT_DATE' },
                    { 'data': 'UPDATE_DATE' },

                ],
           columnDefs: [
               {
                   targets: [4], render: function (data) {
                       return moment(data).format('YYYY-MM-DD');
                   }
               },
               {
                    targets: [ 6, 7], render: function (data) {
                        return moment(data).format('YYYY-MM-DD HH:mm:ss');
                    }
               }
               
           ]
        });

    // Search theo Imei va date // date tu dong loc ngay khi chon ngay // date chua tra ve all data khi xoa ngay
    $('.auto-style3 #submitSearch').click(function () {       
        var imei = $('.auto-style3 #selectIMEI :selected').val();
        table.column(0).search(imei).draw();
    })

    //Loc theo ngay

    var datepickers = [{
        id: 'startdate',
        coid: 'enddate',
        value: null,
        limiter: 'minDate'
    }, {
        id: 'enddate',
        coid: 'startdate',
        value: null,
        limiter: 'maxDate'
    }
    ];
    //Translate 'yy/mm/dd' string to UTC date
    const yymmddUTC = str => new Date(...str.split('/').map((value, index) => index == 1 ? value-- : value));
    //Limit datepicker options to those valid for current dataset
    var dates = table.column(4).data().unique().sort();
    var minDate = dates[0];
    var maxDate = dates[dates.length - 1];
    //datepicker objects definition // dinh dang cho datpicker
    $('.datepicker').datepicker({
      
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        defaultDate: minDate,
        changeYear: true,
        yearRange: minDate.substr(0, 4) + ':' + maxDate.substr(0, 4),
        onSelect: function (selectedDate) {
            let datepicker = datepickers.find(entry => entry.id == $(this).attr('id'));
            $(`#${datepicker.coid}`).datepicker('option', datepicker.limiter, selectedDate);
            datepicker.value = yymmddUTC(selectedDate);
            table.draw();
        }
    }).on('change', function () {
        datepickers[datepickers.findIndex(item => item.id == $(this).attr('id'))].value = yymmddUTC($(this).val());
        table.draw();
    });

    //External search function
    $.fn.DataTable.ext.search.push((settings, row) => {
        let rowDate = yymmddUTC(row[4]);
        return (rowDate >= datepickers[0].value || datepickers[0].value == null) && (rowDate <= datepickers[1].value || datepickers[1].value == null);
    });

   
    
    
};












