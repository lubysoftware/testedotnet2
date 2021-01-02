import { ResultType } from "./result-type";

export class ListSate {
    get done() { return !this.loading && !this.noItems; }

    constructor(
        public loading: boolean = true,
        public noItems: boolean = false,
        public loadingMore: boolean = false,
        public totalItems: number = 0
    ) { }

    reset() {
        if (!this.loading) this.loading = true;
        if (this.noItems) this.noItems = false;
    }

    update(result?: ResultType<any[]>) {
        this.loading = !result || !result.data;
        this.noItems = result && result.data ? result.data.length === 0 : true;
        this.totalItems = result && result.totalRows ? result.totalRows : 0;
    }
}
