import { Component, OnInit, Input, OnDestroy, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';
import { FormGroup, FormBuilder, ValidationErrors, ControlValueAccessor, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable } from 'rxjs';
import * as _ from 'lodash';
import { startWith, map } from 'rxjs/operators';

@Component({
  selector: 'app-autocomplete',
  templateUrl: './autocomplete.component.html',
  styleUrls: ['./autocomplete.component.scss'],
  providers: [{ provide: NG_VALUE_ACCESSOR, useExisting: AutocompleteComponent, multi: true }],
})
export class AutocompleteComponent implements OnInit, OnDestroy, ControlValueAccessor {
  @Input() name: string;
  @Input() group: FormGroup;
  @Input() display: string = 'name';
  @Output() selectionChange = new EventEmitter<any>();
  
  list: any[] = [];
  filtred: Observable<any[]>;
  form: FormGroup;

  constructor(
    private changeDetectionRef: ChangeDetectorRef,
    private formBuilder: FormBuilder,
  ) { }

  ngOnInit() {
    this.validateConfig();
    if (!this.group) this.group = this.formBuilder.group({ id: null, [this.display]: '' });
    this.form = this.formBuilder.group({ field: this.group });
  }

  ngOnDestroy() { }

  setList(list: any[]) {
    this.list = list;
    if (this.group.value.id && !this.group.value[this.display]) {
      const item = this.list.find(i => i.id === this.group.value.id);
      this.group.patchValue({ [this.display]: item[this.display] });
    }
    this.filter();
  }

  validateConfig() {
    if (!this.name) throw 'Config require name';
  }

  displayOption(value?: any): string {
    if (!value) return '';
    return typeof value === 'object' ? value[this.display] : value;
  }

  clear() {
    this.select({ id: null, [this.display]: '' });
  }

  select(item: any) {
    this.onChange(item.id);
    this.group.markAsDirty();
    this.group.patchValue(item);
    this.group.updateValueAndValidity();
    this.selectionChange.emit(item);
  }

  textByOption() {
    const text = _.deburr(this.group.value[this.display].toUpperCase());
    if (!text) return;
    const option = this.list.find(f => _.deburr(f[this.display].toUpperCase()) === text);
    if (!option) return;
    this.select(option);
  }

  filter() {
    this.filtred = this.group.valueChanges
      .pipe(
        startWith<string | any>(''),
        map(value => typeof value === 'string' ? value : value[this.display]),
        map(name => {
          if (typeof name !== 'string') return [];
          if (!name) return this.list.slice();
          const filterValue = _.deburr(name).toLowerCase();
          return this.list.filter(type => _.deburr(type[this.display]).toLowerCase().indexOf(filterValue) !== -1);
        })
      );
    this.changeDetectionRef.detectChanges();
  }

  writeValue(value: string): void {
    if (!value) this.group.patchValue({ id: null, [this.display]: '' });;
    this.group.patchValue({ id: value });
  }
  onChange: any = () => { }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void { }
  setDisabledState?(isDisabled: boolean): void {
    isDisabled
      ? this.group.disable()
      : this.group.enable();
  }
}
