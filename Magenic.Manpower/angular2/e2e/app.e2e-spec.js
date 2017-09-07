"use strict";
var protractor_1 = require('protractor');
describe('App header tests', function () {
    var expectedMsg = 'Manpower App';
    beforeEach(function () {
        protractor_1.browser.get('');
    });
    it('should display: ' + expectedMsg, function () {
        expect(protractor_1.element(protractor_1.by.className('navbar-brand')).getText()).toEqual(expectedMsg);
    });
});
//# sourceMappingURL=app.e2e-spec.js.map