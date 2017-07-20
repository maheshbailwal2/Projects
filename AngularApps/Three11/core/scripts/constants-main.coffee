define [
  'angular'
  'core/app'
  'core/constants/urls'
  'core/constants/http-status'
], (ng)->

  constants = ng.module 'mvWebUi.constants'
  constants.constant 'HttpStatus',
  require 'core/constants/http-status'
  constants.constant 'UserValidationRules',
    name: '^[a-zA-Z-\\s]+$'
    title: '^[a-zA-Z-&().\\s]+$'
    organization: '^[a-zA-Z-&().\\s]+$'
    password: '^.*(?=.{7,20})(?=.*[A-Z])(?=.*[\\d])(?=.*[\\W]).*$'
    # URL regex copied from https://gist.github.com/dperini/729294
    website: '^' +
      '(?:(?:https?|ftp)://)?' +
      '(?:\\S+(?::\\S*)?@)?' +
      '(?:' +
      '(?!(?:10|127)(?:\\.\\d{1,3}){3})' +
      '(?!(?:169\\.254|192\\.168)(?:\\.\\d{1,3}){2})' +
      '(?!172\\.(?:1[6-9]|2\\d|3[0-1])(?:\\.\\d{1,3}){2})' +
      '(?:[1-9]\\d?|1\\d\\d|2[01]\\d|22[0-3])' +
      '(?:\\.(?:1?\\d{1,2}|2[0-4]\\d|25[0-5])){2}' +
      '(?:\\.(?:[1-9]\\d?|1\\d\\d|2[0-4]\\d|25[0-4]))' +
      '|' +
      '(?:(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)' +
      '(?:\\.(?:[a-z\\u00a1-\\uffff0-9]-*)*[a-z\\u00a1-\\uffff0-9]+)*' +
      '(?:\\.(?:[a-z\\u00a1-\\uffff]{2,}))' +
      ')' +
      '(?::\\d{2,5})?' +
      '(?:/\\S*)?' +
      '$'
  return