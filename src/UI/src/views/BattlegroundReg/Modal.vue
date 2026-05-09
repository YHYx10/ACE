<template>
  <div class="modal">
    <div class="modal-wrap">
      <div class="modal-wrap__title">{{loc('BattlegroundReg_7')}}</div>
      <div class="modal-wrap__desc">You have registered for the event.Find out on the plane..</div>
      <DefaultBtn 
        class="modal-wrap__btn"
        @click="closeModal"
      >{{loc('BattlegroundReg_9')}}</DefaultBtn>
    </div>
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import DefaultBtn from '../UI/button/DefaultBtn.vue';
export default {
    name: "Modal",
    computed: {
        ...mapGetters("localization", ["loc"])
    },
    methods: {
        closeModal: function () {
            window.mp.trigger("battlegroundReg:closeModal");
        }
    },
    components: { DefaultBtn },
}
</script>

<style lang="scss" scoped>
.modal{
  @keyframes showModal{
    from {
      opacity: 0
    }
    to {
      opacity: 1;
    }
  }
  position: fixed;
  left: 0;
  top: 0;
  width: 100%;
  height: 100vh;
  background: rgba(0, 0, 0, 0.6);
  display: flex;
  align-items: center;
  justify-content: center;
  animation: showModal .5s;
  .modal-wrap{
    border: 0.1rem solid #5D3FD3;
    box-shadow: inset 0rem 0rem 3.2rem rgba(71, 44, 132, 0.38);
    background-image: url('/img/battlegroundReg/bg-modal.png');
    background-size: contain;
    background-repeat: no-repeat;
    background-position: center;
    width: 31rem;
    height: 18rem;
    display: flex;
    flex-flow: column;
    align-items: center;
    justify-content: center;
    gap: 1.7rem;
    color: #FFFFFF;
    text-transform: uppercase;
    &__title{
      background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0rem 0rem 5.6296rem #BE2020;
      font-weight: 800;
      font-size: 2rem;
      line-height: 2.4rem;
    }
    &__desc{
      font-weight: 800;
      font-size: 1.2rem;
      line-height: 1.45rem;
      text-align: center;
      max-width: 20rem;
    }
    &__btn{
      width: 6rem;
      height: 3rem;
      color: #FFFFFF;
      font-weight: 700;
      font-size: 0.7rem;
      line-height: 0.85rem;
    }
  }
}
</style>
