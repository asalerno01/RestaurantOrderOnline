function timeout(delay) {
    return new Promise( res => setTimeout(res, delay) );
}
async function wait(delay) {
    await timeout(delay); //for 1 sec delay
}
export default function load(setState) {
    wait(500);
    setState(false);
}