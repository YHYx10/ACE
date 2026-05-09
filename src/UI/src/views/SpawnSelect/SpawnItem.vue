<template>

  <div :class="['select_spawn__item', styles[index]]">
    <div class="select_spawn__mores">
      <div class="select_spawn__content">
        <img :src="'/img/spawnSelect/icon-'+icons[index]+'.png'" alt="" class="select_spawn__img" style="margin-bottom: -6px;">
        
        <div class="select_spawn__block">
          <div class="select_spawn__text">
            <h3 class="select_spawn__name">
              {{info[index]}}
            </h3>
            <span v-if="!item" class="select_spawn_disable">Inaccessible</span>
            <p v-if="item" class="select_spawn__subname">
              {{item}}
            </p>
            <p v-else class="select_spawn__subname">
              {{blockedInfo[index]}}
            </p>
          </div>

          <button v-if="item" @click="enter" class="btn btn--select">
            <svg width="12" height="15" viewBox="0 0 12 15" xmlns="http://www.w3.org/2000/svg">
              <path
                d="M11.4986 6.63381L1.49972 0.865224C0.833056 0.480609 0 0.961752 0 1.73141V13.2686C0 14.0382 0.833056 14.5194 1.49972 14.1348L11.4986 8.36619C12.1656 7.98136 12.1656 7.01864 11.4986 6.63381Z" />
            </svg>
          </button>
          <button v-else class="btn btn--select btn--disable">
            <svg width="12" height="15" viewBox="0 0 12 15" xmlns="http://www.w3.org/2000/svg">
              <path
                d="M11.4986 6.63381L1.49972 0.865224C0.833056 0.480609 0 0.961752 0 1.73141V13.2686C0 14.0382 0.833056 14.5194 1.49972 14.1348L11.4986 8.36619C12.1656 7.98136 12.1656 7.01864 11.4986 6.63381Z" />
            </svg>
          </button>

        </div>

      </div>
    </div>
  </div>

</template>

<script>
import { mapGetters } from 'vuex'
export default {
  name: 'SpawnItem',

  props: {
    index: Number,
    item: String
  },

  data: function() {
    return {
      spamProtection: 0,
      info: [
        'Family',
        'Place of entrance',
        'Your home',
        'Faction',
      ],
      styles: [
        'select_spawn__item-family',
        'select_spawn__item-location',
        'select_spawn__item-home',
        'select_spawn__item-fraction'
      ],
      icons:  [
        'family',
        'location',
        'home',
        'fraction'
      ],
      blockedInfo: [
        'You are not in the family',
        '',
        'You have no home',
        'You are not in the Organization',
      ]
    }
  },

  methods: {
    enter: function() {
      if(this.spamProtection > Date.now()) return;
      this.spamProtection = Date.now() + 1000;
      window.mp.triggerServer('auth:char:spawn', this.index)
    }
  },

  computed: {
    ...mapGetters('localization', ['loc'])
  }
}
</script>

<style lang="scss" scoped>
.select_spawn {
  &__item {
    max-width: 51.34099616858238vh;
    width: 43.272335844994615vh;
    height: 20.78902vh;
    
    transition: all 0.3s ease-in-out;
    -webkit-transition: all 0.3s ease-in-out;
    -moz-transition: all 0.3s ease-in-out;
    backface-visibility: hidden;

    &:not(:last-child) {
      margin-bottom: 1.0764262648008611vh;
    }

    &:hover {
      cursor: pointer;
      border: 0.2554278416347382vh solid #301934 ;
      transform: scale(1.05);
    }

    &-active {
      border: 0.2554278416347382vh solid #301934 ;
    }

    &-family {
      border: 0.31928480204342274vh solid rgba(255, 255, 255, 0.09);
      background: rgba(255, 255, 255, 0.01);
      border-radius: 1.1494252873563218vh;
      background-image: url('/img/spawnSelect/bg-family.png');
      background-repeat: no-repeat;
      background-size: cover;
      background-position: center center;
    }

    &-location {
      border: 0.31928480204342274vh solid rgba(255, 255, 255, 0.09);
      background: rgba(255, 255, 255, 0.01);
      border-radius: 1.1494252873563218vh;
      background-image: url('/img/spawnSelect/bg-location.png');
      background-repeat: no-repeat;
      background-size: cover;
      background-position: center center;
    }

    &-home {
      border: 0.31928480204342274vh solid rgba(255, 255, 255, 0.09);
      background: rgba(255, 255, 255, 0.01);
      border-radius: 1.1494252873563218vh;
      background-image: url('/img/spawnSelect/bg-home.png');
      background-repeat: no-repeat;
      background-size: cover;
      background-position: center center;
    }

    &-fraction {
      border: 0.31928480204342274vh solid rgba(255, 255, 255, 0.09);
      background: rgba(255, 255, 255, 0.01);
      border-radius: 1.1494252873563218vh;
      background-image: url('/img/spawnSelect/bg-fraction.png');
      background-repeat: no-repeat;
      background-size: cover;
      background-position: center center;
    }
  }

  &__content {
    padding: 1vh 2.9374201787994894vh 0.2vh 2.9374201787994894vh;
  }

  &__mores {
    height: 100%;
    display: flex;
    justify-content: center;
    flex-direction: column-reverse;
  }

  &__img {
    margin-bottom: 3.8314176245210727vh;
  }

  &__block {
    display: flex;
    align-items: center;
    justify-content: space-between;
  }

  &__text {
    max-width: 26.434227330779056vh;
    position: relative;
  }

  &__name {
    display: inline;
    font-weight: 800;
    font-size: 3.0651340996168583vh;
    line-height: 3.1928480204342273vh;
    text-transform: uppercase;
    background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
    text-fill-color: transparent;
    text-shadow: 0 0 14.303959131545339vh rgba(255, 255, 255, 0.25);
    text-fill-color: transparent;
  }

  &__subname {
    font-weight: 600;
    font-size: 1.6602809706257982vh;
    line-height: 104%;
    text-transform: uppercase;
    color: rgba(255, 255, 255, 0.25);
  }

  &_disable {
    margin-left: 1vh;
    vertical-align: bottom;
    display: inline-block;
    background: linear-gradient(180deg, rgba(71, 44, 132, 0.25) 0%, rgba(75, 0, 130, 0.25) 100%);
    border: 0.1277139208173691vh solid #301934 ;
    border-radius: 0.5108556832694764vh;
    padding: 0.5108556832694764vh 1.0217113665389528vh;
    font-weight: 700;
    font-size: 1.6602809706257982vh;
    line-height: 2.0434227330779056vh;
    text-transform: uppercase;
    color: #301934 ;
  }
}

.btn {
  cursor: pointer;
  border: unset;
  //transition: .2s;

  &--select {
    background: linear-gradient(180deg, rgba(71, 44, 132, 0.25) 0%, rgba(75, 0, 130, 0.25) 100%);
    border: 0.1277139208173691vh solid #301934 ;
    box-shadow: inset 0 0 1.9157088122605364vh rgba(75, 0, 130, 0.86);
    border-radius: 1.1494252873563218vh;
    padding: 2.554278416347382vh;
    display: flex;
    align-items: center;
    justify-content: center;

    svg {
      fill: #fff;
    }

    &:hover {
      background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    }
  }

  &--disable {
    background: linear-gradient(180deg, rgba(223, 223, 223, 0.25) 0%, rgba(123, 123, 123, 0.25) 100%);
    border: 0.1277139208173691vh solid rgba(238, 238, 238, 0.25);
    box-shadow: none;
    border-radius: 1.1494252873563218vh;
    padding: 2.554278416347382vh;
    display: flex;
    align-items: center;
    justify-content: center;

    svg {
      fill: rgba(#fff, 0.25);

    }

    &:hover {
      background: linear-gradient(180deg, rgba(192, 192, 192, 0.25) 0%, rgba(112, 112, 112, 0.25) 100%);
      svg {
        fill: #fff;
      }
    }
  }
}
</style>
