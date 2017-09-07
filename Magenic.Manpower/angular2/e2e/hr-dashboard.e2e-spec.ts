import { browser, element, by } from 'protractor';

describe('HR Dashboard route testing', function () {
    let expectedMsg = 'Current Requests';

    it('should display: ' + expectedMsg + 'when routing to / or empty', function () {
        browser.get('');
        expect(element(by.id('cr-head')).getText()).toEqual(expectedMsg);
    });

    it('should display: ' + expectedMsg + 'when routing to /hrdashboard', function () {
        browser.get('/hrdashboard');
        expect(element(by.id('cr-head')).getText()).toEqual(expectedMsg);
    });
});
