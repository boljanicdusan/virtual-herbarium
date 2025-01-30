import { Directive, HostListener } from '@angular/core';

@Directive({
    selector: '[appDisableDrag]'
})
export class DisableDragDirective {
    @HostListener('dragstart', ['$event'])
    onDragStart(event: DragEvent): void {
        event.preventDefault();
    }
}
