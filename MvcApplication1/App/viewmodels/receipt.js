define(['services/logger'], function (logger) {
    var title = 'Receipt Builder';
    var storeName = ko.observable;
    var receiptDate = ko.observable;
    var productBrand = ko.observable;
    var productName = ko.observable;
    var productPrice = ko.observable;
    var productQuantity = ko.observable;
    var productUnit = ko.observable;

    var vm = {
        activate: activate,
        title: title
    };

    return vm;

    //#region Internal Methods
    function activate() {
        logger.log(title + ' View Activated', null, title, true);
        return true;
    }

    function addItem() {
        IngredientController.GetAllIngredients();
    }

    //#endregion
});