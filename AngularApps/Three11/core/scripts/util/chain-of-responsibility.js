// Generated by CoffeeScript 1.9.2
(function() {
  'use strict';
  define(['lodash'], function(_, Assert) {
    return function() {
      var _funcDefs, _locals, createChain;
      _locals = arguments[0];
      _funcDefs = [];
      createChain = function($injector) {
        return _.reduce(_funcDefs, function(head, funcCtor) {
          return _.partialRight($injector.invoke(funcCtor, null, _locals), head || void 0);
        }, null);
      };
      return {
        $get: ['$injector', createChain],
        push: function(func) {
          _funcDefs.push(func);
          return this;
        }
      };
    };
  });

}).call(this);