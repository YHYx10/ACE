<template>
  <img @click="$emit('click')" src="/img/common/exit.svg" alt="EXIT">
</template>

<style scoped>
img {
  opacity: .55;
  transition: 0.5s ease;
}
img:hover {
  opacity: 1;
}
</style>