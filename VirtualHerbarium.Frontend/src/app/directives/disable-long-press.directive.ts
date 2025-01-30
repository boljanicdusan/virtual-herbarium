import { Directive, HostListener } from '@angular/core';

@Directive({
  selector: '[appDisableLongPress]'
})
export class DisableLongPressDirective {
  @HostListener('touchmove', ['$event'])
  preventDefault(event: Event): void {
    event.preventDefault();
  }
}
