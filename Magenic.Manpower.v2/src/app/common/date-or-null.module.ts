import { NgModule } from '@angular/core';
import { DateOrNull } from './date-or-null.pipe';
import { DatePipe } from '@angular/common';

@NgModule({
    imports: [],
    declarations: [DateOrNull],
    exports: [DateOrNull]
})

export class DateOrNullModule {
    static forRoot() {
        return {
            ngModule: DateOrNullModule,
            providers: [DatePipe]
        };
    }
}