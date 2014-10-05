function IndexViewModel() {
    // Data
    var self = this;
    self.Ingredients = ko.observableArray([]);
    self.storeNameText = ko.observable();
    self.dateText = ko.observable();
    self.timeText = ko.observable();
    self.brandText = ko.observable();
    self.itemText = ko.observable();
    self.priceText = ko.observable();
    self.quantityText = ko.observable();
    self.unitText = ko.observable();

    self.addIngredient = function () {
        self.Ingredients.push(new Ingredient({ name: this.itemText() }));
        self.itemText("");
    };
};

ko.applyBindings(new IndexViewModel());