<template>
  <div class="col-center">
    <Title text="Railing management" style="transform: translate(0, -50%);" />
    <div class="list">
      <div class="item title">
        <div class="text">
       Fügen Sie den Rang hinzu
        </div>
        <div class="icons" @click="addNewRank">
          <img src="/img/familyMenu/rank-add.svg" alt="add" />
        </div>
      </div>
      <div class="item" v-for="item in ranksList" :key="item.key">
        <div class="text">
          {{ item.rankName }}
        </div>
        <div class="icons">
          <img
            v-if="isLeader"
            @click="goToRank(item)"
            src="/img/familyMenu/rank-edit.svg"
            alt="edit"
          />
          <img
            v-if="isLeader && item.rankId !== 0"
            @click="deleteRank(item.rankId)"
            src="/img/familyMenu/rank-delete.svg"
            alt="delete"
          />
        </div>
      </div>
    </div>
    <modal-control
      v-if="showModal"
      @closeModal="closeModal"
      :rank="currentRank"
    />
  </div>
</template>

<script>
import Title from './components/Title.vue'
import { mapGetters, mapState } from 'vuex'
import ModalControl from './modals/ModalRank.vue'

export default {
  components: { Title, ModalControl },
  computed: {
    ...mapState('familyMenu/controlPage', ['ranksList']),
    ...mapState('familyMenu', ['isLeader']),
    ...mapGetters('localization', ['loc']),
  },

  data: function() {
    return {
      showModal: false,
      currentRank: null,
    }
  },

  methods: {
    setModal: function() {
      this.showModal = !this.showModal
    },
    closeModal: function() {
      this.setModal()
      this.currentRank = null
    },
    goToRank: function(obj) {
      this.currentRank = obj
      this.setModal()
    },
    addNewRank: function() {
      window.mp.trigger('familyMenu:addNewRank')
    },
    deleteRank: function(rank) {
      window.mp.trigger('familyMenu:deleteRank', rank)
    },
  },
}
</script>

<style lang="scss" scoped>
div,
span,
button,
input {
  font-family: 'Akrobat';
  font-style: normal;
  color: #ffffff;
}
.col-center {
  width: 44.537vh;
  height: 100%;
  position: relative;
  .list {
    margin-top: 0.741vh;
    margin-left: 2.778vh;
    overflow-y: scroll;
    width: 39.907vh;
    height: 70.37vh;
    &::-webkit-scrollbar {
      width: 0.463vh;
    }
    &::-webkit-scrollbar-track {
      background: rgba(255, 255, 255, 0.04);
    }
    &::-webkit-scrollbar-thumb {
      background: #301934 ;
    }
    .item {
      margin-bottom: 0.926vh;
      width: 38.519vh;
      height: 7.87vh;
      display: flex;
      justify-content: space-between;
      align-items: center;
      border: 0.093vh solid;
      box-sizing: border-box;
      position: relative;
      border-image: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.09) 0%,
          rgba(255, 255, 255, 0) 101.25%
        )
        1 1 stretch;
      overflow: hidden;
      &::before {
        content: '';
        position: absolute;
        width: 42.315vh;
        height: 2.037vh;
        left: 50%;
        top: 50%;
        transform: translate(-50%, -50%);
        background: rgba(255, 255, 255, 0.55);
        filter: blur(8.241vh);
        z-index: -1;
      }
      &.title {
        border: none;
        background: linear-gradient(90deg, #0b4517 0%, #277738 100%);
        margin-bottom: 1.852vh;
      }
      .text {
        margin-left: 2.593vh;
        font-weight: 700;
        font-size: 2.222vh;
        line-height: 2.685vh;
        text-transform: uppercase;
      }
      .icons {
        margin-right: 2.778vh;
        display: flex;
        align-items: center;
        gap: 1.852vh;
        img {
          width: 2.222vh;
          opacity: 0.75;
        }
        img:hover {
          opacity: 1;
        }
      }
    }
  }

  &::after {
    content: '';
    position: absolute;
    top: 0;
    right: 0;
    width: 0.185vh;
    height: 100%;
    background: linear-gradient(
      180deg,
      rgba(255, 255, 255, 0.2) 70%,
      rgba(255, 255, 255, 0) 100%
    );
  }
}
</style>
