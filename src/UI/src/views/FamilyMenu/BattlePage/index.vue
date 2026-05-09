<template>
  <div class="battle-page">
    <modal-notify
      v-if="modalNotify.show"
      :data="unAcceptedWars"
      @closeModalNotify="modalNotify.show = false"
    />
    <div class="battle-page__info-panel">
      <div class="about">
        <div class="item">
          <img
            class="item attack"
            src="/img/familyMenu/battlePage/icon-attack.png"
          />
          <div class="text">attack</div>
        </div>
        <div class="item">
          <img
            class="item defense"
            src="/img/familyMenu/battlePage/icon-defense.png"
          />
          <div class="text">schutz</div>
        </div>
      </div>

      <div class="table-title">
        <div>{{ loc('fam:btl:panel:1') }}</div>
        <div>{{ loc('fam:btl:panel:2') }}</div>
        <div>{{ loc('fam:btl:panel:3') }}</div>
        <div>{{ loc('fam:btl:panel:4') }}</div>
        <div>{{ loc('fam:btl:panel:5') }}</div>
        <div>{{ loc('fam:btl:panel:6') }}</div>
      </div>
    </div>
    <div ref="scrollParent" class="battle-page__content">
      <div class="content-pending">
        <battle-row
          v-for="(item, index) in pendingWars"
          :key="Date.now() + index"
          :data="item"
        />
        <div class="shift-element"></div>
        <battle-row
          v-for="(item, index) in completedWars"
          :key="Date.now() + 10000 + index"
          :data="item"
        />
      </div>
    </div>
  </div>
</template>

<script>
import BattleRow from './components/BattleRow.vue'
import ModalNotify from './components/ModalNotify'

import { mapGetters, mapState } from 'vuex'

export default {
  name: 'BattlePage',
  data() {
    return {
      modalNotify: {
        show: false,
      },
    }
  },
  components: {
    BattleRow,
    ModalNotify,
  },
  computed: {
    ...mapState('familyMenu/battlePage', [
      'pendingWars',
      'completedWars',
      'unAcceptedWars',
    ]),
    ...mapGetters('localization', ['loc']),
  },
  mounted() {
    // this.modalNotify.show = this.unAcceptedWars.length > 0;
    setTimeout(() => {
      this.modalNotify.show = this.unAcceptedWars.length > 0
    }, 500)
  },
}
</script>

<style lang="scss" scoped>
.battle-page {
  width: 100%;
  font-family: 'Akrobat';
  position: relative;
  display: flex;
  justify-content: center;
  flex-direction: column;
  align-items: center;
  &__info-panel {
    width: 141.111vh;
  }

  &__content {
    overflow: scroll;

    &::-webkit-scrollbar {
      display: none;
    }

    .content-pending {
      width: fit-content;
      padding-right: 1.111vh;
      height: 61.481vh;
      overflow-y: scroll;
      &::-webkit-scrollbar {
        width: 0.463vh;
      }
      &::-webkit-scrollbar-track {
        background: rgba(255, 255, 255, 0.04);
      }
      &::-webkit-scrollbar-thumb {
        background: #301934 ;
      }
      .shift-element {
        margin-bottom: 1.852vh;
      }
      .battle-row {
        margin-bottom: 0.463vh;
      }
    }
  }

  .about {
    display: flex;
    margin-top: 5.833vh;
    margin-left: 8.056vh;
    margin-bottom: 1.852vh;
    gap: 3.611vh;
    .item {
      display: flex;
      align-items: center;
      gap: 1.296vh;
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.315vh;
      &.attack {
        width: 3.333vh;
        margin-right: 0.37vh;
      }
      &.defense {
        width: 4.259vh;
      }
    }
  }

  .table-title {
    display: flex;
    color: rgba(255, 255, 255, 0.5);
    font-weight: 700;
    font-size: 1.852vh;
    line-height: 2.315vh;
    padding: 2.13vh 0;
    div:nth-child(1) {
      margin-left: 19.259vh;
      width: 26.019vh;
    }
    div:nth-child(2) {
      width: 17.5vh;
    }
    div:nth-child(3) {
      width: 11.111vh;
    }
    div:nth-child(4) {
      width: 9.63vh;
    }
    div:nth-child(5) {
      width: 13.056vh;
    }
  }
}
</style>
