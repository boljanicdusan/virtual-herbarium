import { Directive, HostListener } from '@angular/core';

@Directive({
    selector: '[appDisableRightClick]'
})
export class DisableRightClickDirective {
    @HostListener('contextmenu', ['$event'])
    onRightClick(event: MouseEvent): boolean {
        event.preventDefault();
        return false;
    }
}
