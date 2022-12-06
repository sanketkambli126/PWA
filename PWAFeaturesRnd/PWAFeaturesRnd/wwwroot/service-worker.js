var idb = function (e) { "use strict"; let t, n; const r = new WeakMap, o = new WeakMap, s = new WeakMap, a = new WeakMap, i = new WeakMap; let c = { get(e, t, n) { if (e instanceof IDBTransaction) { if ("done" === t) return o.get(e); if ("objectStoreNames" === t) return e.objectStoreNames || s.get(e); if ("store" === t) return n.objectStoreNames[1] ? void 0 : n.objectStore(n.objectStoreNames[0]) } return p(e[t]) }, set: (e, t, n) => (e[t] = n, !0), has: (e, t) => e instanceof IDBTransaction && ("done" === t || "store" === t) || t in e }; function u(e) { return e !== IDBDatabase.prototype.transaction || "objectStoreNames" in IDBTransaction.prototype ? (n || (n = [IDBCursor.prototype.advance, IDBCursor.prototype.continue, IDBCursor.prototype.continuePrimaryKey])).includes(e) ? function (...t) { return e.apply(f(this), t), p(r.get(this)) } : function (...t) { return p(e.apply(f(this), t)) } : function (t, ...n) { const r = e.call(f(this), t, ...n); return s.set(r, t.sort ? t.sort() : [t]), p(r) } } function d(e) { return "function" == typeof e ? u(e) : (e instanceof IDBTransaction && function (e) { if (o.has(e)) return; const t = new Promise((t, n) => { const r = () => { e.removeEventListener("complete", o), e.removeEventListener("error", s), e.removeEventListener("abort", s) }, o = () => { t(), r() }, s = () => { n(e.error || new DOMException("AbortError", "AbortError")), r() }; e.addEventListener("complete", o), e.addEventListener("error", s), e.addEventListener("abort", s) }); o.set(e, t) }(e), n = e, (t || (t = [IDBDatabase, IDBObjectStore, IDBIndex, IDBCursor, IDBTransaction])).some(e => n instanceof e) ? new Proxy(e, c) : e); var n } function p(e) { if (e instanceof IDBRequest) return function (e) { const t = new Promise((t, n) => { const r = () => { e.removeEventListener("success", o), e.removeEventListener("error", s) }, o = () => { t(p(e.result)), r() }, s = () => { n(e.error), r() }; e.addEventListener("success", o), e.addEventListener("error", s) }); return t.then(t => { t instanceof IDBCursor && r.set(t, e) }).catch(() => { }), i.set(t, e), t }(e); if (a.has(e)) return a.get(e); const t = d(e); return t !== e && (a.set(e, t), i.set(t, e)), t } const f = e => i.get(e); const l = ["get", "getKey", "getAll", "getAllKeys", "count"], D = ["put", "add", "delete", "clear"], v = new Map; function b(e, t) { if (!(e instanceof IDBDatabase) || t in e || "string" != typeof t) return; if (v.get(t)) return v.get(t); const n = t.replace(/FromIndex$/, ""), r = t !== n, o = D.includes(n); if (!(n in (r ? IDBIndex : IDBObjectStore).prototype) || !o && !l.includes(n)) return; const s = async function (e, ...t) { const s = this.transaction(e, o ? "readwrite" : "readonly"); let a = s.store; r && (a = a.index(t.shift())); const i = a[n](...t); return o && await s.done, i }; return v.set(t, s), s } return c = (e => ({ ...e, get: (t, n, r) => b(t, n) || e.get(t, n, r), has: (t, n) => !!b(t, n) || e.has(t, n) }))(c), e.deleteDB = function (e, { blocked: t } = {}) { const n = indexedDB.deleteDatabase(e); return t && n.addEventListener("blocked", () => t()), p(n).then(() => { }) }, e.openDB = function (e, t, { blocked: n, upgrade: r, blocking: o, terminated: s } = {}) { const a = indexedDB.open(e, t), i = p(a); return r && a.addEventListener("upgradeneeded", e => { r(p(a.result), e.oldVersion, e.newVersion, p(a.transaction)) }), n && a.addEventListener("blocked", () => n()), i.then(e => { s && e.addEventListener("close", () => s()), o && e.addEventListener("versionchange", () => o()) }).catch(() => { }), i }, e.unwrap = f, e.wrap = p, e }({});

const objectStore = {
    htmlCachedData: {
        name: "htmlCachedData",
        keyPath: "requestUrl"
    },
    modelCachedData:
    {
        name: "modelCachedData",
        keyPath: "requestUrl"
    }
}
var vshipDb;

async function createDB() {
    const db = idb.openDB("Test", 1, {
        upgrade(db) {
            db.createObjectStore("htmlCachedData");
            db.createObjectStore("modelCachedData");
            db.createObjectStore("PSCDeficiency");
            db.createObjectStore("ChatNotificationList");
            db.createObjectStore("ChatNotificationDetails");
            db.createObjectStore('ChatChannelsDeleted');
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

// Update 'version' if you need to refresh the cache
var version = 'v1.0::CacheFirstSafe';
var offlineUrl = "/offline.html";

// Store core files in a cache (including a page to display when offline)
function updateStaticCache() {
    return caches.open(version)
        .then(function (cache) {
            return cache.addAll([
                offlineUrl,

            ]);
        });
}

function addToCache(request, response) {
    if (!response.ok && response.type !== 'opaque')
        return;

    var copy = response.clone();
    caches.open(version)
        .then(function (cache) {
            cache.put(request, copy);
        });
}

function serveOfflineImage(request) {
    if (request.headers.get('Accept').indexOf('image') !== -1) {
        return new Response('<svg role="img" aria-labelledby="offline-title" viewBox="0 0 400 300" xmlns="http://www.w3.org/2000/svg"><title id="offline-title">Offline</title><g fill="none" fill-rule="evenodd"><path fill="#D8D8D8" d="M0 0h400v300H0z"/><text fill="#9B9B9B" font-family="Helvetica Neue,Arial,Helvetica,sans-serif" font-size="72" font-weight="bold"><tspan x="93" y="172">offline</tspan></text></g></svg>', { headers: { 'Content-Type': 'image/svg+xml' } });
    }
}

self.addEventListener('install', function (event) {
    self.skipWaiting()
});

self.addEventListener('activate', function (event) {
    event.waitUntil(
        createDB()
    );
});

self.addEventListener('fetch', function (event) {
    var request = event.request;
    if (request.url.match("/Dashboard/GetInspectionFleetSummary") ||
        request.url.match("/Dashboard/GetCrewFleetSummary") ||
        request.url.match("/Dashboard/GetOpexFleetSummary") ||
        request.url.match("/Dashboard/GetHazOccFleetSummary") ||
        request.url.match("/Dashboard/GetCommercialFleetSummary") ||
        request.url.match("/Dashboard/GetRightshipFleetSummary") ||
        request.url.match("/Dashboard/GetPMSFleetSummary") ||
        request.url.match("/Dashboard/GetFleetSummary") ||
        request.url.match("/Dashboard/GetPSCDeficiencies") ||
        request.url.match("/Dashboard/GetOverdueInspectionDetails") ||
        request.url.match("/Dashboard/GetExperienceMatrixDetails") ||
        request.url.match("/Dashboard/GetCriticalPMS")
    ) {

        event.respondWith(
            fetch(request)
                .then(async function (response) {
                    await addToCache(request, response);
                    return Promise.resolve(response);
                })
                .catch(async function () {
                    let response = await caches.match(request.url);
                    return Promise.resolve(response)
                })
        );
        return;
    }
    else if (request.url.match("/Dashboard/GetRightShipDetails")) {
        event.respondWith(
            fetch(request)
                .then(async function (response) {
                    let updatedresponse = await addhtmlCachedToDb(request, response);
                    return Promise.resolve(updatedresponse);
                })
                .catch(async function () {
                    if (!vshipDb) {
                        await createDB();
                    }
                    var data = await vshipDb.get(objectStore.htmlCachedData.name, request.url.replace(request.referrer, ""))
                    let init = { "status": 200, "statusText": "" };
                    let newResponse = new Response(JSON.stringify(data.data), init)
                    return Promise.resolve(newResponse);
                })
        );
        return;
    }

    else if (request.url.match("/Dashboard/GetSeriousIncidents")) {
        event.respondWith(
            fetch(request)
                .then(async function (response) {
                    var url = new URL(request.url);
                    var channelId = url.searchParams.get("channelId");


                })
                .catch(async function () {
                    if (!vshipDb) {
                        await createDB();
                    }
                    var data = await vshipDb.get(objectStore.modelCachedData.name, request.url.replace(request.referrer, ""))
                    let init = { "status": 200, "statusText": "" };
                    let newResponse = new Response(JSON.stringify(data.data), init)
                    return Promise.resolve(newResponse);
                })
        );
        return;
    }
    else if (request.url.match('/Notification/DeleteChannelById')) {
        event.respondWith(
            fetch(request)
                .then(async function (response) {
                    if (!vshipDb) {
                        await createDB();
                    }
                    let url = new URL(request.url);
                    let channelId = url.searchParams.get("channelId")
                    let data = await vshipDb.get('ChatChannelsDeleted', channelId);
                    deleteChannelFromChatList(channelId);
                    if (data) {
                        vshipDb.delete('ChatChannelsDeleted', channelId)
                    }
                    return Promise.resolve(response);
                }).catch(async function () {
                    if (!vshipDb) {
                        await createDB();
                    }
                    let url = new URL(request.url);
                    let channelId = url.searchParams.get("channelId")
                    vshipDb.put('ChatChannelsDeleted', channelId, channelId);
                    deleteChannelFromChatList(channelId);
                    let init = { "status": 200, "statusText": "" };
                    let newResponse = new Response(JSON.stringify({ success: true }), init)
                    return Promise.resolve(newResponse);
                })
        );
    }

    // Always fetch non-GET requests from the network
    else if (request.method !== 'GET' || request.url.match(/\/browserLink/ig)) {
        event.respondWith(
            fetch(request)
                .catch(function () {
                    return caches.match(offlineUrl);
                })
        );
        return;
    }

    // For HTML requests, try the network first, fall back to the cache, finally the offline page
    else if (request.headers.get('Accept').indexOf('text/html') !== -1) {
        event.respondWith(
            fetch(request)
                .then(function (response) {
                    // Stash a copy of this page in the cache
                    addToCache(request, response);
                    return response;
                })
                .catch(function () {
                    return caches.match(request)
                        .then(function (response) {
                            return response || caches.match(offlineUrl);
                        });
                })
        );
        return;
    }

    // cache first for fingerprinted resources
    else if (request.url.match(/(\?|&)v=/ig)) {
        event.respondWith(
            caches.match(request)
                .then(function (response) {
                    return response || fetch(request)
                        .then(function (response) {
                            addToCache(request, response);
                            return response || serveOfflineImage(request);
                        })
                        .catch(function () {
                            return serveOfflineImage(request);
                        });
                })
        );

        return;
    }

    // network first for non-fingerprinted resources
    else event.respondWith(
        fetch(request)
            .then(function (response) {
                // Stash a copy of this page in the cache
                addToCache(request, response);
                return response;
            })
            .catch(function () {
                return caches.match(request)
                    .then(function (response) {
                        return response || serveOfflineImage(request);
                    })
                    .catch(function () {
                        return serveOfflineImage(request);
                    });
            })
    );
});

self.addEventListener("push", event => {

    if (event.data) {

        var payload = event.data.json();
        var title = "PWA Demo";
        var options = {
            body: payload.Msg,
            icon: payload.Icon
        };

        event.waitUntil(self.registration.showNotification(title, options));
    }
});


async function addTodb(request, response) {
    let data = await response.json();
    let dataToAdd = { requestUrl: request.url.replace(request.referrer, ""), data: data }
    if (!vshipDb) {
        await createDB();
    }
    let OfflineDB = await vshipDb.put(objectStore.modelCachedData.name, dataToAdd.requestUrl, dataToAdd);

    let init = { "status": 200, "statusText": "" };
    let newResponse = new Response(JSON.stringify(data), init)
    return Promise.resolve(newResponse);
}

async function addhtmlCachedToDb(request, response) {
    let data = await response.json();
    let dataToAdd = { requestUrl: request.url.replace(request.referrer, ""), data: data }
    if (!vshipDb) {
        await createDB();
    }
    let OfflineDB = await vshipDb.put(objectStore.htmlCachedData.name, dataToAdd.requestUrl, dataToAdd);

    let init = { "status": 200, "statusText": "" };
    let newResponse = new Response(JSON.stringify(data), init)
    return Promise.resolve(newResponse);
}

async function deleteChannelFromChatList(channelId) {
    if (!vshipDb) {
        await createDB();
    }
    let offlineData = await vshipDb.getAll('ChatNotificationList');
    offlineData.map(function (d, idx) {
        return { channelId: d.channelId, key: idx }
    }).filter(function (e) {
        return e.channelId == channelId
    }).forEach(function (e) {
        vshipDb.delete('ChatNotificationList', e.key);
    })

    return Promise.resolve()
}