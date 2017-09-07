import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({ name: 'dateOrNull' })
export class DateOrNull implements PipeTransform {
    constructor(private datePipe: DatePipe) { }
    transform(value: string, pattern: string, nullStr: string): string {
        let hasValue = value && new Date(Date.parse(value)) > new Date(1, 1, 1, 0, 0, 0, 0);
        return hasValue ? this.datePipe.transform(value, pattern) : nullStr;
    }
}
