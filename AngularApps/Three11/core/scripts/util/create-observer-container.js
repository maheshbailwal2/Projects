// Generated by CoffeeScript 1.9.2
(function() {
  define(['lodash', 'core/util/assert'], function(_, Assert) {
    return function($log) {
      var _id, detach, theContainer;
      theContainer = {};
      _id = -1;
      detach = function(id) {
        delete theContainer[id];
      };
      Object.defineProperties(theContainer, {
        append: {
          value: function(func) {
            Assert["true"](_.isFunction(func), 'function expected');
            theContainer[++_id] = func;
            return _.bind(detach, null, _id);
          }
        },
        isEmpty: {
          get: function() {
            return _.isEmpty(theContainer);
          }
        },
        notify: {
          value: function() {
            var args;
            args = Array.prototype.slice.call(arguments);
            return _.forIn(theContainer, function(func) {
              var err;
              try {
                return func.apply(null, args);
              } catch (_error) {
                err = _error;
                if ($log != null) {
                  $log.debug("About to throw error: " + err);
                }
                throw err;
              }
            });
          }
        }
      });
      return theContainer;
    };
  });

}).call(this);
