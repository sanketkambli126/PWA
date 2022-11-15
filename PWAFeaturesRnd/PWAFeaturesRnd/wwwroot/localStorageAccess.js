export const dbName = "Vships_PWA";
export const objectsStores = {
    keyindicators: { name: 'cachedIndictors', keyPath: 'requestUrl' }
}
export var idb
export function createDb(dbName, versionNo) {
    const request = window.indexedDB.open(dbName, versionNo)
    request.onupgradeneeded = function (e) {
        idb = e.target.result

        for (var key of Object.keys(objectsStores)) {
            var obj = objectsStores[key];
            db.createObjectStore(obj.name, { keyPath: obj.keyPath });
        }
    }

    request.onerror = function (e) {
        console.error('Unable to open database.');
    }

    request.onsuccess = function (e) {
        idb = e.target.result;
        console.log('db opened');
    }

    return idb;
}