'use strict';
describe('rolesCtrl', function() {
  beforeEach(module('rolesModule'));

  var $controller;

  beforeEach(inject(function(_$controller_){
    // The injector unwraps the underscores (_) from around the parameter names when matching
    $controller = _$controller_;
  }));

  describe('$ctrl.create', function() {
    it('sets modal label and modal button save', function() {
      var $scope = {};
      var controller = $controller('rolesCtrl', { $scope: $scope });
      controller.create();
      expect(controller.editModalLabel).toEqual('New Role');
      expect(controller.editModalSave).toEqual('Save');
      expect(controller.role.name).toEqual('');
    });
  });
});