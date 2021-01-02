import { HttpParams } from '@angular/common/http';

export class Rest {
    static GetParams(obj: any): { params: HttpParams } {
        let params = new HttpParams();
        Object.keys(obj).forEach((key) => {
            const value = obj[key];
            if ([null, undefined].indexOf(value) !== -1)
                return;

            if (value instanceof Date) {
                params = params.append(key, value.toISOString());
                return;
            }
            params = params.append(key, value);
        });
        return { params };
    }
}
