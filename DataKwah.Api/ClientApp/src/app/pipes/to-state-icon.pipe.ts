import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'toStateIcon'
})
export class ToStateIconPipe implements PipeTransform {

  transform(value: number, ...args: unknown[]): string {
    switch (value) {
      case 0:
        return '<i class="pi pi-question" title="requested"></i>';
      case 1:
        return '<i class="pi pi-play" title="pending"></i>';
      case 2:
        return '<i class="pi pi-check" title="done"></i>';
      case 3:
        return '<i class="pi pi-times" title="failed"></i>';
      default:
        return '-';
    }
  }
}
