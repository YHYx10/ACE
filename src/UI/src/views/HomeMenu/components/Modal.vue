<template>
  <div class="modal-wrap">
    <div class="modal">
      <div class="btn-close" @click="closeModal"></div>
      <template v-if="type === 'rentCost'">
        <div class="title" v-if="type === 'rentCost'">{{loc('HomeMenu_18')}}</div>
        <input type="text" v-model.number="currentCost" :placeholder="loc('home:menu:plh:1')">
        <div class="btn-apply" @click="setCurrentCost">{{loc('HomeMenu_19')}}</div>
      </template>
      <template v-else>
        <div class="title">{{loc('HomeMenu_20')}}</div>
        <input type="text" v-model="currentOccupier" :placeholder="loc('home:menu:plh:2')">
        <div class="btn-apply" @click="setCurrentOccupier">{{loc('HomeMenu_19')}}</div>
      </template>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapMutations, mapState } from 'vuex'
export default {
  name: 'Modal',
  
  props: {
    type: String,
    closeModal: Function
  },

  data: function() {
    return {
      currentCost: null,
      currentOccupier: null,
    }
  },
  computed: {
    ...mapState('homeMenu', ['houseId']),
    ...mapMutations('homeMenu', ['setRentCost', 'rentCost']),
    ...mapGetters('localization', ['loc']),    
  },
  methods: {
    setCurrentOccupier: function() {
      window.mp.triggerServer("house:occupierAddedRequest", this.houseId, this.currentOccupier)
      this.closeModal()
    },
    setCurrentCost: function() {
      this.closeModal()
      window.mp.trigger("homeMenu:rentCostChanged", this.houseId, this.currentCost)
    }
  }
}
</script>

<style lang="scss" scoped>
.modal-wrap{
  position: fixed;
  display: flex;
  align-items: center;
  justify-content: center;
  top: 0;
  left: 0;
  height: 100vh;
  width: 100%;
  background: rgba(0, 0, 0, 0.65);
  z-index: 10;
  .modal{
    background: #0a0a0a;
    box-shadow: 0 1.1rem 1.6rem rgba(0, 0, 0, 0.35);
    display: flex;
    flex-flow: column;
    align-items: start;
    padding: 3rem 4.75rem 3rem 1.75rem;
    position: relative;
    width: 36rem;
    min-width: 36rem;
    .btn-close{
      position: absolute;
      z-index: 1;
      top: 1.5rem;
      right: 1.5rem;
      width: 2rem;
      height: 2rem;
      background-size: contain;
      background-repeat: no-repeat;
      background-position: center;
      background-image: url('/img/homeMenu/btn-close.svg');
      opacity: .45;
      &:hover{
        transition: .2s;
        opacity: 1;
      }
    }
    .title{
      position: relative;
      text-transform: uppercase;
      margin-left: 1rem;
      &:before {
        content: '';
        position: absolute;
        height: 100%;
        top: 0;
        left: -1rem;
        color: #fff;
        background: #fff;
        border: 1px solid #fff;
        box-shadow: 0px 0px 14px rgba(255, 255, 255, 0.55);
      }
    }
    input{
      border: 1px solid;
      font-weight: 600;
      border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.1) 30%, rgba(0, 0, 0, 0) 90%) 1;
      background: linear-gradient(90deg, rgba(255, 255, 255, 0) 0%, rgba(255, 255, 255, 0.03) 30%, rgba(12, 16, 10, 0) 100%);
      box-sizing: border-box;
      width: 100%;
      padding: 1.35rem;
      margin: 2.25rem 0;
      color: #fff;
    }
    .btn-apply{
      text-align: center;
      cursor: pointer;
      letter-spacing: 0.03em;
      font-size: 1.3rem;
      white-space: nowrap;
      color: #fff;
      padding: 0.7rem 3.5rem;
      background: linear-gradient(180deg, rgba(71, 44, 132, 0.25) 0%, rgba(75, 0, 130, 0.25) 100%);
      border: 1px solid #301934 ;
      box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);
      transition: 0.5s ease;

      &:hover {
        box-shadow: inset 0px 0px 7.5rem #301934 ;
        filter: drop-shadow(0px 0px 15px rgba(71, 44, 132, 0.5));
      }
    }
  }
}
</style>
