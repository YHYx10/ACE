<template>
  <div :class="[{active: currentContract && item.ContractName === currentContract.ContractName}, 'contract-item']">
    <div 
      class="contract-item__img"
      :style="{background: `linear-gradient(0deg, rgba(0,0,0,0) 20%, rgba(0,0,0,0.7) 60%), url(/img/optionsMenu/programTab/contracts/${item.Image}) center / cover no-repeat`}"
    ></div>
    <div class="contract-item__heading">
      <div class="subtitle">Name</div>
      <div class="title">{{item.Name}}</div>
      <div class="subtitle"> A brief description for an example</div>
      <div
          class="contract-item__date"
          v-if="contractState && contractState.InProgress"
      >It is expiring {{contractState.ExpirationDate.toLocaleString("ru", options)}}</div>
    </div>
    <div
      class="item__btn"
      @click="setCurrentContract"
    >Read more</div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
export default {
  name: 'ContractItem',
  data: function () {
    return {
      options: {
        month: 'long',
        day: 'numeric',
        hour: 'numeric',
        minute: 'numeric',
      },
    }
  },
  props: {
    item: Object,
    currentContract: Object
  },
  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('optionsMenu', ['myContracts', 'familyContracts']),
    contractState() {
      if (this.item.ContractType === "Family")
        return this.familyContracts.find(c => c.ContractName == this.item.ContractName);
      if (this.item.ContractType === "Single")
        return this.myContracts.find(c => c.ContractName == this.item.ContractName);
      return null;
    }
  },
  methods: {
    setCurrentContract: function() {
      this.$emit('setCurrentContract', this.item)
    }
  }
}
</script>

<style lang="scss" scoped>
.contract-item{
  display: flex;
  flex-flow: column nowrap;
  align-items: start;
  height: 18rem;
  border: 1px solid rgba(255, 255, 255, 0.1);
  padding: 1rem 1.5rem 1.5rem;
  position: relative;

  &.active {
    .item__btn {
      box-shadow: inset 0px 0px 7.5rem #301934 ;
      filter: drop-shadow(0px 0px 15px rgba(71, 44, 132, 0.5));
    }
  }
  &>div{
    z-index: 1;
  }
  &__img{
    width: 100%;
    height: 100%;
    position: absolute;
    left: 0;
    top: 0;
    z-index: 0;
  }
  &__date{
    color: #301934 ;
    margin-top: 0.5rem;
    font-weight: 800;
    font-size: 1.1rem;
  }
  &__heading {
    position: relative;
    .title {
      font-size: 1.8rem;
      line-height: 1.9rem;
    }
  }
  .item__btn {
    position: relative;
    margin-top: auto;
  }
}
</style>
