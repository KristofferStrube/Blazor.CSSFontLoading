export function getAttribute(object, attribute) { return object[attribute]; }

export function constructFontFaceSet(initialFaces) {
    return new FontFaceSet(initialFaces);
}

export function arrayFrom(values) {
    var res = []
    for (let value of values) {
        res.push(value);
    }
    console.log(res);
    return res;
}

export function arrayBufferFrom(array) {
    return array.buffer
}