import * as _ from 'underscore';
import { FormControl } from '@angular/forms';

export class FormValidators {

    constructor(
    ) {


    }

    static emailValidator(control: FormControl) {

        if (!_.isEmpty(control.value) && !control.value.match(/[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?/)) {
            return { InvalidEmailAddress: true };
        }
    }


}
