export class TimePicker {
    hour: number;
    minute: number;

    constructor(date?: Date, hour?: number, minute?: number) {
      if (hour != null && minute != null) {
        this.hour = hour;
        this.minute = minute;
      }
      if (date) {
        this.hour = date.getHours();
        this.minute = date.getMinutes();
      }
    }

    public toMinute() {
      return this.hour * 60 + this.minute;
    }
}
export class DatePicker {
    year: number;
    month: number;
    day: number;

    constructor(date?: Date, year?: number, month?: number, day?: number) {
        if (year != null && month != null && day != null) {
        this.year = year;
        this.month = month;
        this.day = day;
        }

        if (date) {
        this.year = date.getFullYear();
        this.month = date.getMonth();
        this.day = date.getDate();
        }

        this.month += 1;
    }

    toDate() {
        return new Date(this.year, this.month - 1, this.day);
    }
}
export class DateTimePicker {
    date: DatePicker;
    time: TimePicker;

    constructor (date: Date) {
        this.date = new DatePicker(date);
        this.time = new TimePicker(date);
    }

    toDate() {
        return new Date(this.date.year, this.date.month - 1, this.date.day, this.time.hour, this.time.minute);
    }
}