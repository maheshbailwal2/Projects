// Generated by CoffeeScript 1.9.2
(function() {
  'use strict';
  define(function() {
    var IgnoreKey, stripFunctions;
    IgnoreKey = {
      _links: true,
      $$hashKey: true
    };
    return stripFunctions = function(obj) {
      return _.omit(obj, function(v, k) {
        return IgnoreKey[k] || _.isFunction(v);
      });
    };
  });

}).call(this);
