// Generated by CoffeeScript 1.9.2
(function() {
  'use strict';
  define(['lodash'], function(_) {
    return function(obj) {
      _.forIn(obj, function(value, key) {
        if (!_.isFunction(value)) {
          delete obj[key];
        }
      });
      return obj;
    };
  });

}).call(this);
