<template>
  <div @click="$emit('onSelect')" class="weapon-item" :class="{active: selected}">
    <img :src="img" alt="" />
    <div class="about">
      <div class="name">{{ name }}</div>
      <div class="ammo">{{ ammo }}</div>
    </div>
  </div>
</template>

<script>
export default {
  props: {
    img: String,
    name: String,
    ammo: String,
    selected: Boolean
  },
}
</script>

<style lang="scss" scoped>
.weapon-item {
  width: 35.648vh;
  height: 16.296vh;
  display: flex;
  flex-direction: column;
  padding: 0 1.852vh;
  background: rgba(217, 217, 217, 0.05);
  align-items: center;
  &.active {
    background: linear-gradient(180deg, rgba(71, 44, 132, 0.8) 0%, rgba(75, 0, 130, 0.5) 100%);
  }
  img {
    margin-top: 1.944vh;
    width: 24.907vh;
    height: 9.63vh;
  }
  .about {
    width: 100%;
    margin-top: 1.296vh;
    display: flex;
    justify-content: space-between;
    font-family: 'Akrobat';
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.315vh;
    text-transform: uppercase;
    color: #FFFFFF;
  }
}
</style>
