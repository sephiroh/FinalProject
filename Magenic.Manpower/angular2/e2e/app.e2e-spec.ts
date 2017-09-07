import { browser, element, by } from 'protractor';

describe('App header tests', function () {
    let expectedMsg = 'Manpower App';
    beforeEach(function () {
        browser.get('');
    });
    it('should display: ' + expectedMsg, function () {
        expect(element(by.className('navbar-brand')).getText()).toEqual(expectedMsg);
    });
});
