// Generated by CoffeeScript 1.9.2
(function() {
  define(['core/controllers/modal'], function() {
    return function(constantsModule, controllersModule, featureName, models) {
      var capitalizedModelName, controllerName, fn, modalOptionsName, modalStateNamePrefix, modelConfig, modelName, stateId;
      fn = function(modalOptionsName, controllerName) {
        constantsModule.constant(stateId, modalStateNamePrefix);
        constantsModule.constant(modalOptionsName, modelConfig);
        return controllersModule.controller(controllerName, ['$scope', 'modalStateModel', 'mvModalService', modalOptionsName, stateId, require('core/controllers/modal')]);
      };
      for (modelName in models) {
        modelConfig = models[modelName];
        capitalizedModelName = modelName[0].toUpperCase() + modelName.slice(1);
        modalOptionsName = "" + featureName + capitalizedModelName + "ModalOptions";
        controllerName = "" + featureName + capitalizedModelName + "ModalController";
        modalStateNamePrefix = "" + featureName + capitalizedModelName;
        stateId = "" + featureName + capitalizedModelName + "State";
        fn(modalOptionsName, controllerName);
      }
    };
  });

}).call(this);