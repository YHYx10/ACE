<template>
  <div class="window">
    <ExitCross @click="exit" class="exit-cross" />
    <div class="frisk-menu">
      <div class="header">
        <img src="/img/friskInterface/lupa.png" alt="" class="magnifier" />
        <div class="title">Search for a citizen</div>
        <div class="name">{{ playerName || 'Test_Testovich' }}</div>
        <div class="id">id: {{ playerId || -1 }}</div>
      </div>
      <div class="content" :class="{ 'not-found': !showButton }">
        <div class="wrap">
          <div
            class="cell"
            v-for="(item, index) in items"
            :key="index"
            :class="{ active: item.id != '-1' }"
          >
            <div
              class="item"
              :style="{
                'background-image':
                  item.id != '-1' ? `url(${getImage(item.id)})` : 'none',
              }"
            ></div>
            <div class="count" v-if="isStackable(item.id)">
              x{{ item.count }}
            </div>
          </div>
        </div>
      </div>

      <div class="state" :class="{ between: showButton }">
        <div class="message">
          {{
            showButton
              ? 'You have found illegal objects'
              : 'You have not found anything'
          }}
        </div>
        <button @click="takeAll" v-if="showButton">Remove</button>
      </div>
    </div>
  </div>
</template>

<script>
import configs from '../../configs/inventory/configs'
import ExitCross from '../UI/components/ExitCross.vue'

import { mapGetters, mapState } from 'vuex'

export default {
  name: 'FriskInterface',

  components: {
    ExitCross,
  },

  data: function() {
    return {
      showButton: false,
      fixGrid: true,
      closeHovered: false,
    }
  },

  computed: {
    ...mapGetters('localization', ['loc']),
    ...mapState('friskInterface', [
      'items',
      'playerName',
      'playerId',
      'dataLoaded',
    ]),
  },

  mounted() {
    console.log(this.items.length + ' ' + (this.items.length % 8))
    if (this.items.length <= 24) {
      if (this.items.length > 0) {
        this.showButton = true
      }
      while (this.items.length != 24) {
        this.items.push({ id: '-1', count: 0 })
      }
    } else {
      this.showButton = true
      this.fixGrid = false
    }
    if (this.items.length % 8 != 0) {
      while (this.items.length % 8 != 0) {
        this.items.push({ id: '-1', count: '-1' })
      }
    }
  },

  methods: {
    exit() {
      window.mp.trigger('friskInterface:close')
    },
    takeAll() {
      window.mp.trigger('friskInterface:takeAll')
    },
    getImage(id) {
      return `/img/inventory/items/${configs[id].Image}.png`
    },
    isStackable(id) {
      if (id == '-1') return false
      return configs[id].Stackable
    },
  },
}
</script>

<style lang="scss" scoped>
.exit-cross {
  position: absolute;
  top: 2rem;
  right: 2rem;
  z-index: 10;
}

.window {
  width: 100vw;
  height: 100vh;
  background: rgba(15, 17, 21, 0.82);
  display: flex;
  align-items: center;
  justify-content: center;
  &::after {
    content: '';
    background-image: url('/img/friskInterface/lenta.png');
    background-size: cover;
    position: absolute;
    top: 0;
    left: 0;
    z-index: 1;
    width: 100%;
    height: 100%;
    opacity: 0.02;
  }
}
.frisk-menu {
  position: relative;
  z-index: 2;
  width: 42.65rem;
  .header {
    height: 8.85rem;
    display: flex;
    flex-direction: column;
    align-items: center;
    background: rgba(28, 29, 34, 0.9);
    border-radius: 0.1rem;
    position: relative;
    color: #ffffff;
    img.magnifier {
      position: absolute;
      transform: translate(2.75rem, -3.15rem);
      right: 0;
      width: 13.75rem;
      height: 11.7rem;
    }
    .title {
      margin-top: 1.95rem;
      font-weight: 800;
      font-size: 1.6rem;
      line-height: 1.9rem;
      text-transform: uppercase;
    }
    .name {
      font-weight: 600;
      font-size: 1rem;
      line-height: 1.2rem;
      margin-top: 1rem;
    }

    .id {
      font-weight: 600;
      font-size: 1rem;
      line-height: 1.2rem;
      color: rgba(255, 255, 255, 0.5);
      text-transform: uppercase;
    }
  }

  .content,
  .header {
    background: rgba(28, 29, 34, 0.9);
    border-radius: 0.1rem;
  }
  .content {
    margin-top: 1rem;
    height: 22.1rem;
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
    &.not-found::after {
      content: '';
      background-image: url('/img/friskInterface/lenta.png');
      background-size: cover;
      position: absolute;
      top: 0;
      left: 0;
      z-index: 1;
      width: 100%;
      height: 100%;
    }

    .wrap {
      display: grid;
      gap: 1rem 0.5rem;
      height: 14rem;
      grid-template-columns: repeat(8, 1fr);
      overflow-y: auto;
      overflow-x: hidden;
      padding-right: 0.4rem;
      &::-webkit-scrollbar {
        width: 0.1rem;
      }
      &::-webkit-scrollbar-track {
        background-color: rgba(255, 255, 255, 0.01);
      }
      &::-webkit-scrollbar-thumb {
        background-color: rgba(255, 255, 255, 0.1);
      }
      .cell {
        width: 4rem;
        height: 4rem;
        background: radial-gradient(
          50% 50% at 50% 50%,
          rgba(0, 0, 0, 0.05) 0%,
          rgba(0, 0, 0, 0.09) 100%
        );
        border: 0.05rem solid rgba(255, 255, 255, 0.1);
        border-radius: 0.1rem;
        &.active {
          background: radial-gradient(
            50% 50% at 50% 50%,
            rgba(0, 0, 0, 0.07) 0%,
            rgba(0, 0, 0, 0.11) 100%
          );
          border: 0.05rem solid rgba(255, 255, 255, 0.2);
        }
        display: flex;
        align-items: center;
        justify-content: center;
        position: relative;
        .item {
          width: 80%;
          height: 80%;
          background-size: cover;
        }
        .count {
          position: absolute;
          bottom: 0.1rem;
          right: 0.2rem;
          color: #ffffff;
        }
      }
    }
  }
  .state {
    display: flex;
    justify-content: center;
    text-align: center;
    &.between {
      text-align: left;
      justify-content: space-between;
    }
    width: 100%;
    margin-top: 1.5rem;
    .message {
      color: #ffffff;
      text-shadow: 0 0 0.1rem black;
      font-weight: 800;
      font-size: 1.6rem;
      line-height: 1.9rem;
      width: 17.1rem;
      text-transform: uppercase;
    }

    button {
      &::before {
        content: '';
        position: absolute;
        width: 100%;
        height: 100%;
        background: rgba(28, 29, 34, 0.2);
        z-index: -1;
        top: 0;
        left: 0;
      }
      position: relative;
      text-transform: uppercase;
      width: 14.75rem;
      height: 3.75rem;
      font-weight: 700;
      font-size: 1.2rem;
      line-height: 1.45rem;
      color: #ffffff;
      border: 0.093vmin solid rgba($color: #301934 , $alpha: 1);
      background: linear-gradient(
        rgba($color: #301934 , $alpha: 0.25),
        rgba($color: #591b87, $alpha: 0.25)
      );
      transition: 0.3s ease;
      box-shadow: inset 0 0 1.389vmin rgba($color: #301934 , $alpha: 0.86);

      &:hover {
        transition: 0.3s ease;
        box-shadow: inset 0rem 0rem 150px #301934 ;
        filter: drop-shadow(0rem 0rem conv(15) rgba(71, 44, 132, 0.5));
      }
    }
  }
}
</style>
