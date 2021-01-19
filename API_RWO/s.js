let f
setTimeout(() => {
  f = 1
}, 5000)
console.log(f)
setTimeout(() => {
  console.log(f)
}, 6000)