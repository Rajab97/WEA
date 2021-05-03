var selectedId;
var check = true;

var dtGrid = {
    instance: function () {
        return $("#dtGrid").dxDataGrid("instance");
    },

    selectionChanged: function (selectedItems, callback) {
        var data = selectedItems.selectedRowsData[0];
        if (typeof data === 'undefined') {
            selectedId = null;
            return;
        }
        selectedId = data.Id;
        dtGrid.reloadButtons(data);
        if (typeof selectionChangedCallBack !== "undefined") {
            selectionChangedCallBack(data);
        }
    },

    reloadButtons: function (data) {
        /*if (data) {
            if (data.CanDelete) $("#btnRemove").prop("disabled", false);
            else $("#btnRemove").prop("disabled", true);

            if (data.CanEdit) $("#btnEdit").prop("disabled", false);
            else $("#btnEdit").prop("disabled", true);

            if (data.CanPrint) $("#btnPrint").prop("disabled", false);
            else $("#btnPrint").prop("disabled", true);

            if (data.CanExecute) $("#btnExecute").prop("disabled", false);
            else $("#btnExecute").prop("disabled", true);

            $("#btnDetails").prop("disabled", false);
        }
        else {
            $("#btnEdit").prop("disabled", true);
            $("#btnRemove").prop("disabled", true);
            $("#btnPrint").prop("disabled", true);
            $("#btnExecute").prop("disabled", true);
            $("#btnDetails").prop("disabled", true);
        }*/
    },

    reload: function () {
        selectedId = null;

        $("#dtGrid").dxDataGrid("instance").selectRows([], false);

        $("#dtGrid").dxDataGrid("getDataSource").reload();

        dtGrid.reloadButtons(null);

        //if (selectedId)
        //    $("#dtGrid").dxDataGrid("instance").selectRows([selectedId], true);

        //var selectedRows = $("#dtGrid").dxDataGrid("instance").getSelectedRowsData();
        //if (selectedRows)
        //    dtGrid.reloadButtons(selectedRows[0]);
    },

    filter: function () {
        var dataGrid = $("#dtGrid").dxDataGrid("instance");
        dataGrid.clearFilter();
        if (check) {
            dataGrid.option("filterRow.visible", true);
            check = false;
        }
        else {
            dataGrid.option("filterRow.visible", false);
            check = true;
        }
    },
    filterCustom: function () {
        var dataGrid = $("#dtGrid").dxDataGrid("instance");
        //dataGrid.clearFilter();
        if (check) {
            dataGrid.option("filterRow.visible", true);
            check = false;
        }
        else {
            dataGrid.option("filterRow.visible", false);
            check = true;
        }
    },

    idexRowTemplate: function (element, info) {
        element.html(info.rowIndex + 1);
    },

    onContentReady: function (e) {
        e.element.find(".dx-datagrid-text-content").removeClass("dx-text-content-alignment-left");
    },

    exportToExcel: function (fileName) {
        var date = new Date();
        if (!fileName) {
            fileName = "Çıxarış_" + + ("0" + date.getDate()).slice(-2) + ("0" + (date.getMonth() + 1)).slice(-2) + date.getFullYear() + ("0" + date.getHours()).slice(-2) + ("0" + date.getMinutes()).slice(-2);
        }
        else {
            fileName = fileName + "_" + ("0" + date.getDate()).slice(-2) + ("0" + (date.getMonth() + 1)).slice(-2) + date.getFullYear() + ("0" + date.getHours()).slice(-2) + ("0" + date.getMinutes()).slice(-2);
        }
        var dataGrid = $("#dtGrid").dxDataGrid("instance");
        dataGrid.option("export.fileName", fileName);
        dataGrid.exportToExcel();
    }
};



var devExtreme = {
    setDataSource: function (instance, value, actionUrl, paramName) {
        var dataSource = DevExpress.data.AspNet.createStore({
            "loadParams": { [paramName]: value },
            "loadUrl": "" + actionUrl + "",
            "key": "Id"
        });
        instance.option('dataSource', dataSource);
        instance.reset();
    },
    reloadDataSource: function (e) {
        e.component.getDataSource().reload();
    },
    scrollToError: function () {
        let validatedInput = $("input[aria-invalid=true]")
        let label = $("label[for=" + $(validatedInput[0]).attr("id") + "]");

        if ($(label).length > 0) {
            $([document.documentElement, document.body]).animate({
                scrollTop: $(label).offset().top - $(".navbar-custom").height()
            }, 500);
        }
    },
    onSelectBoxScrollView: function (e) {
        var list = e.component._list;
        if (!list.option('useNativeScrolling')) {
            list.option('useNativeScrolling', true);
            list._scrollView.option('useNative', true);
            list.reload();
        }
    }
};