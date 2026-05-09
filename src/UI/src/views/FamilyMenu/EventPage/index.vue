<template>
  <div class="event-page">
    <transition name="fading">
      <div v-if="showNavElem != 'BattleResults'" class="nav">
        <div
          :class="[{ active: showNavElem === 'PrivateEvents' }, 'item']"
          @click="toggleNavElem('PrivateEvents')"
        >
          {{ loc('fam:menu:events:priv') }}
        </div>
        <div
          :class="[{ active: showNavElem === 'GeneralEvents' }, 'item']"
          @click="toggleNavElem('GeneralEvents')"
        >
          {{ loc('fam:menu:events:comm') }}
        </div>
      </div>
    </transition>

    <GeneralEvents />
    <PrivateEvents />

    <battle-results v-if="showBattleResults" />
  </div>
</template>

<script>
import { mapState, mapMutations, mapGetters } from 'vuex'

import BattleResults from './views/BattleResults'
import GeneralEvents from './views/GeneralEvents'
import PrivateEvents from './views/PrivateEvents'

export default {
  name: 'EventPage',
  components: {
    PrivateEvents,
    GeneralEvents,
    BattleResults,
  },
  computed: {
    ...mapState('familyMenu/eventsPage', ['showBattleResults', 'showNavElem']),
    ...mapGetters('localization', ['loc']),
  },
  methods: {
    ...mapMutations('familyMenu/eventsPage', ['toggleNavElem']),
  },
  mounted() {},
}
</script>

<style lang="scss" scoped>
// anim for dummy
@keyframes isShow1 {
  0% {
    opacity: 0;
  }
  25% {
    opacity: 1;
  }
  100% {
    opacity: 1;
  }
}
@keyframes isShow2 {
  0% {
    opacity: 0;
  }
  25% {
    opacity: 0;
  }
  50% {
    opacity: 0;
  }
  100% {
    opacity: 1;
  }
}

.fading-enter-active,
.fading-leave-active {
  transition: all 0.3s ease;
}
.fading-enter,
.fading-leave-to {
  opacity: 0;
}

.event-page {
  width: 171.852vh;
  margin-left: auto;
  margin-right: auto;
  .nav {
    margin-top: 5.556vh;
    display: flex;
    align-items: center;
    gap: 1.852vh;
    .item {
      display: flex;
      justify-content: center;
      align-items: center;
      transition: 0.2s;
      width: 24.815vh;
      height: 7.407vh;
      border: 0.093vh solid rgba(255, 255, 255, 0.09);
      font-family: 'Akrobat';
      font-weight: 700;
      font-size: 1.852vh;
      line-height: 2.222vh;
      text-transform: uppercase;
      margin-bottom: 3.704vh;
      color: #ffffff;

      &:hover {
        border: 0.093vh solid rgba(255, 255, 255, 0.15);
      }
      &.active {
        background: rgba(255, 255, 255, 0.09);
        border: 0.093vh solid rgba(255, 255, 255, 0.15);
      }
      &:last-child {
        margin-right: 0;
      }
    }
  }
}
</style>
