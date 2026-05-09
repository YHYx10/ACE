<template>
  <div class="modal">
    <MainCurrencyModalCard @close="close()" />
    <div class="object-items">
      <ItemCurrrencyCard v-for="item in money" :key="item.id" :objectCard="item" />
    </div>
  </div>
</template>

<script>
import MainCurrencyModalCard from '@/views/OptionsMenu/Tabs/ShopTab/CurrencyGameModal/commons/MainCurrencyModalCard'
import ItemCurrrencyCard from './commons/ItemCurrrencyCard.vue';
export default {
  name: 'CurrencyGameModal',
  components: { MainCurrencyModalCard, ItemCurrrencyCard },
  props: {
        money: Array
  },
  mounted(){
    console.log('money: '+this.money);
  },
  methods: {
    close(){
      this.$emit("close");
    }
  }
}
</script>

<style lang="scss" scoped>
.modal {
  position: absolute;
  /*display: flex;*/
  top: -0.09rem;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 100;
  // background: url('/img/optionsMenu/bg.png'), rgba(0, 0, 0, 0.96);
  background-blend-mode: overlay;
  display: flex;
  gap: 7.407vh;
}

.object-items {
    display: flex;
    // max-width: 54.693rem;
    // max-height: 41.697rem;
    flex-wrap: wrap;
    max-width: 56.693rem;
    max-height: 41.697rem;
    justify-content: space-between;
    // gap: 3.426vh;
}
.active {
  
}
</style>
