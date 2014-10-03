﻿define(['services/logger'], function (logger) {
    var title = 'Receipt';
    var vm = {
        activate: activate,
        title: title
    };

    return vm;

    //#region Internal Methods
    function activate() {
        logger.log(title + ' View Activated', null, title, true);
        $.getJSON('api/Receipt/GetAllIngredients');
        return true;
    }
    //#endregion
});