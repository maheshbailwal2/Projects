// Generated by CoffeeScript 1.9.2
(function() {
  'use strict';
  define(function() {
    var hostUrl;
    hostUrl = /^http(s)?:\/\/[^\/]+\//i;
    return function(s) {
      return (s || '').replace(hostUrl, '');
    };
  });

}).call(this);