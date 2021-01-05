export class Progress {
    loading: boolean;
    current: number;
    total: number;

    constructor() {
        this.loading = false;
        this.total = 0;
        this.current = 0;
    }

    percent() {
        if (this.total === 0) {
            return 0;
        }

        if (this.current > this.total) {
            return 100;
        }

        return this.current * 100 / this.total;
    }

    initLoading(total: number) {
        this.total = total;
        this.loading = true;
        this.current = 0;
    }

    increaseCurrent() {
        this.current++;
        if (this.current === this.total) {
            // this.loading = false;
        }
    }
}
