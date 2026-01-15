# `performance`

Import:

```ts
import { performance } from "@tsonic/nodejs/index.js";
```

Example:

```ts
import { console, performance } from "@tsonic/nodejs/index.js";

export function main(): void {
  const start = performance.now();
  // ...work...
  const end = performance.now();
  console.log(`elapsed: ${end - start}ms`);
}
```

## API Reference

<!-- API:START -->
### `MarkOptions`

```ts
export interface MarkOptions {
    get detail(): unknown | undefined;
    set detail(value: unknown);
    startTime: Nullable<System_Internal.Double>;
}

export const MarkOptions: {
    new(): MarkOptions;
};
```

### `MeasureOptions`

```ts
export interface MeasureOptions {
    get detail(): unknown | undefined;
    set detail(value: unknown);
    end: Nullable<System_Internal.Double>;
    get endMark(): string | undefined;
    set endMark(value: string);
    start: Nullable<System_Internal.Double>;
    get startMark(): string | undefined;
    set startMark(value: string);
}

export const MeasureOptions: {
    new(): MeasureOptions;
};
```

### `performance`

```ts
export declare const performance: {
  clearMarks(name?: string): void;
  clearMeasures(name?: string): void;
  getEntries(): PerformanceEntry[];
  getEntriesByName(name: string, type?: string): PerformanceEntry[];
  getEntriesByType(type: string): PerformanceEntry[];
  mark(name: string, options?: MarkOptions): PerformanceMark;
  measure(name: string, startOrOptions?: unknown, endMark?: string): PerformanceMeasure;
  now(): double;
};
```

### `PerformanceEntry`

```ts
export interface PerformanceEntry {
    readonly duration: double;
    readonly entryType: string;
    readonly name: string;
    readonly startTime: double;
}

export const PerformanceEntry: {
    new(): PerformanceEntry;
};
```

### `PerformanceMark`

```ts
export interface PerformanceMark extends PerformanceEntry {
    readonly detail: unknown | undefined;
}

export const PerformanceMark: {
    new(name: string, startTime: double, detail: unknown): PerformanceMark;
};
```

### `PerformanceMeasure`

```ts
export interface PerformanceMeasure extends PerformanceEntry {
    readonly detail: unknown | undefined;
}

export const PerformanceMeasure: {
    new(name: string, startTime: double, duration: double, detail: unknown): PerformanceMeasure;
};
```

### `PerformanceObserver`

```ts
export interface PerformanceObserver {
    disconnect(): void;
    observe(options: PerformanceObserverOptions): void;
    takeRecords(): PerformanceObserverEntryList;
}

export const PerformanceObserver: {
    new(callback: Action<PerformanceObserverEntryList, PerformanceObserver>): PerformanceObserver;
    supportedEntryTypes(): string[];
};
```

### `PerformanceObserverEntryList`

```ts
export interface PerformanceObserverEntryList {
    getEntries(): PerformanceEntry[];
    getEntriesByName(name: string, type?: string): PerformanceEntry[];
    getEntriesByType(type: string): PerformanceEntry[];
}

export const PerformanceObserverEntryList: {
    new(entries: PerformanceEntry[]): PerformanceObserverEntryList;
};
```

### `PerformanceObserverOptions`

```ts
export interface PerformanceObserverOptions {
    buffered: boolean;
    get entryTypes(): string[] | undefined;
    set entryTypes(value: string[]);
}

export const PerformanceObserverOptions: {
    new(): PerformanceObserverOptions;
};
```
<!-- API:END -->
