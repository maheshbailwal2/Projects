// Generated by CoffeeScript 1.9.2
(function() {
  define(['lodash', 'core/util/assert'], function(_, Assert) {
    return function(target, model) {
      var defineProperty, enumerable, obj;
      obj = Object;
      defineProperty = _.bind(obj.defineProperty, null, target);
      enumerable = {
        enumerable: true
      };
      _.forIn(model, function(prop, key) {
        var boundProp, funcProp;
        Assert["false"](_.has(target, key), 'property already defined');
        if (_.isFunction(prop)) {
          funcProp = {
            value: function() {
              return model[key].apply(model, _.toArray(arguments));
            }
          };
          return defineProperty(key, _.extend(funcProp, enumerable));
        } else {
          boundProp = {
            get: function() {
              return model[key];
            },
            set: function(value) {
              return model[key] = value;
            }
          };
          return defineProperty(key, _.extend(boundProp, enumerable));
        }
      });
      return target;
    };
  });

}).call(this);