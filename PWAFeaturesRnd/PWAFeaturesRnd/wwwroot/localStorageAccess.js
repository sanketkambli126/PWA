﻿var idb = function (e) { "use strict"; let t, n; const r = new WeakMap, o = new WeakMap, s = new WeakMap, a = new WeakMap, i = new WeakMap; let c = { get(e, t, n) { if (e instanceof IDBTransaction) { if ("done" === t) return o.get(e); if ("objectStoreNames" === t) return e.objectStoreNames || s.get(e); if ("store" === t) return n.objectStoreNames[1] ? void 0 : n.objectStore(n.objectStoreNames[0]) } return p(e[t]) }, set: (e, t, n) => (e[t] = n, !0), has: (e, t) => e instanceof IDBTransaction && ("done" === t || "store" === t) || t in e }; function u(e) { return e !== IDBDatabase.prototype.transaction || "objectStoreNames" in IDBTransaction.prototype ? (n || (n = [IDBCursor.prototype.advance, IDBCursor.prototype.continue, IDBCursor.prototype.continuePrimaryKey])).includes(e) ? function (...t) { return e.apply(f(this), t), p(r.get(this)) } : function (...t) { return p(e.apply(f(this), t)) } : function (t, ...n) { const r = e.call(f(this), t, ...n); return s.set(r, t.sort ? t.sort() : [t]), p(r) } } function d(e) { return "function" == typeof e ? u(e) : (e instanceof IDBTransaction && function (e) { if (o.has(e)) return; const t = new Promise((t, n) => { const r = () => { e.removeEventListener("complete", o), e.removeEventListener("error", s), e.removeEventListener("abort", s) }, o = () => { t(), r() }, s = () => { n(e.error || new DOMException("AbortError", "AbortError")), r() }; e.addEventListener("complete", o), e.addEventListener("error", s), e.addEventListener("abort", s) }); o.set(e, t) }(e), n = e, (t || (t = [IDBDatabase, IDBObjectStore, IDBIndex, IDBCursor, IDBTransaction])).some(e => n instanceof e) ? new Proxy(e, c) : e); var n } function p(e) { if (e instanceof IDBRequest) return function (e) { const t = new Promise((t, n) => { const r = () => { e.removeEventListener("success", o), e.removeEventListener("error", s) }, o = () => { t(p(e.result)), r() }, s = () => { n(e.error), r() }; e.addEventListener("success", o), e.addEventListener("error", s) }); return t.then(t => { t instanceof IDBCursor && r.set(t, e) }).catch(() => { }), i.set(t, e), t }(e); if (a.has(e)) return a.get(e); const t = d(e); return t !== e && (a.set(e, t), i.set(t, e)), t } const f = e => i.get(e); const l = ["get", "getKey", "getAll", "getAllKeys", "count"], D = ["put", "add", "delete", "clear"], v = new Map; function b(e, t) { if (!(e instanceof IDBDatabase) || t in e || "string" != typeof t) return; if (v.get(t)) return v.get(t); const n = t.replace(/FromIndex$/, ""), r = t !== n, o = D.includes(n); if (!(n in (r ? IDBIndex : IDBObjectStore).prototype) || !o && !l.includes(n)) return; const s = async function (e, ...t) { const s = this.transaction(e, o ? "readwrite" : "readonly"); let a = s.store; r && (a = a.index(t.shift())); const i = a[n](...t); return o && await s.done, i }; return v.set(t, s), s } return c = (e => ({ ...e, get: (t, n, r) => b(t, n) || e.get(t, n, r), has: (t, n) => !!b(t, n) || e.has(t, n) }))(c), e.deleteDB = function (e, { blocked: t } = {}) { const n = indexedDB.deleteDatabase(e); return t && n.addEventListener("blocked", () => t()), p(n).then(() => { }) }, e.openDB = function (e, t, { blocked: n, upgrade: r, blocking: o, terminated: s } = {}) { const a = indexedDB.open(e, t), i = p(a); return r && a.addEventListener("upgradeneeded", e => { r(p(a.result), e.oldVersion, e.newVersion, p(a.transaction)) }), n && a.addEventListener("blocked", () => n()), i.then(e => { s && e.addEventListener("close", () => s()), o && e.addEventListener("versionchange", () => o()) }).catch(() => { }), i }, e.unwrap = f, e.wrap = p, e }({});

const objectStore = {
    htmlCachedData: {
        name: "htmlCachedData",
        keyPath: "requestUrl"
    },
    modelCachedData:
    {
        name: "modelCachedData",
        keyPath: "requestUrl"
    },
    PSCDeficiency:
    {
        name: "storePSCDeficiency",
        keyPath: "id"
    }
}
var vshipDb;

async function createDB() {
    const db = idb.openDB("Test", 1, {
        upgrade(db) {
            db.createObjectStore("htmlCachedData");
            db.createObjectStore("modelCachedData")
        },
    });

    vshipDb = {
        get: async (storeName, key) => (await db).transaction(storeName).store.get(key),
        getAll: async (storeName) => (await db).transaction(storeName).store.getAll(),
        getFirstFromIndex: async (storeName, indexName, direction) => {
            const cursor = await (await db).transaction(storeName).store.index(indexName).openCursor(null, direction);
            return (cursor && cursor.value) || null;
        },
        put: async (storeName, key, value) => (await db).transaction(storeName, 'readwrite').store.put(value, key === null ? undefined : key),
        putAllFromJson: async (storeName, json) => {
            const store = (await db).transaction(storeName, 'readwrite').store;
            JSON.parse(json).forEach(item => store.put(item));
        },
        delete: async (storeName, key) => (await db).transaction(storeName, 'readwrite').store.delete(key),
        autocompleteKeys: async (storeName, text, maxResults) => {
            const results = [];
            let cursor = await (await db).transaction(storeName).store.openCursor(IDBKeyRange.bound(text, text + '\uffff'));
            while (cursor && results.length < maxResults) {
                results.push(cursor.key);
                cursor = await cursor.continue();
            }
            return results;
        }
    };
    return Promise.resolve();
}