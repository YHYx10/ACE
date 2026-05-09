<template>
  <div class="property-page">
    <template v-if="propertyList.length > 0">
      <div class="property-list">
        <div
          :class="[
            { active: item.id === currentProperty.id },
            'property-list__item',
          ]"
          v-for="item in propertyList"
          :key="item.key"
          @click="setCurrentProperty(item)"
        >
          {{ item.name }}
        </div>
      </div>
      <div class="center">
        <div
          class="img"
          :style="{
            backgroundImage:
              'url(/img/familyMenu/propertyPage/property-' +
              currentProperty.img +
              '.png)',
          }"
        ></div>
        <div class="row">
          <DefaultBtn @click="setOnGps">
            <img src="/img/familyMenu/propertyPage/gps.svg" alt="" />
            <div>Setzen Sie die Route ein</div>
          </DefaultBtn>
          <TitleItem name="Owner" :value="[currentProperty.owner]" :fontSize="24" />
        </div>
      </div>
      <div class="left">
        <div class="biz-name">
          {{ currentProperty.name }}
        </div>
        <div class="id">ID: {{ currentProperty.id }}</div>
        <div class="stats">
          <div
            class="item"
            v-for="(item, index) in currentProperty.stats"
            :key="item.key"
            :class="`item-${index}`"
          >
            <img :src="`/img/familyMenu/propertyPage/ico-${index}.svg`" alt="" />
            <TitleItem :name="item.desc" :value="[item.value]" :fontSize="48" />
          </div>
        </div>
      </div>
    </template>
    <div v-else class="empty-list">{{ loc('fam:menu:prop:notfound') }}</div>
  </div>
</template>

<script>
import { mapState, mapGetters } from 'vuex'
import TitleItem from '../components/TitleItem'
import DefaultBtn from '../../UI/button/DefaultBtn'

export default {
  name: 'PropertyPage',

  data: function() {
    return {
      currentProperty: {},
    }
  },

  components: {
    TitleItem,
    DefaultBtn,
  },

  computed: {
    ...mapState('familyMenu/propertyPage', ['propertyList']),
    ...mapGetters('localization', ['loc']),
  },

  methods: {
    setCurrentProperty: function(obj) {
      this.currentProperty = { ...obj }
    },
    setOnGps: function() {
      window.mp.trigger('familyMenu:setOnGps', this.currentProperty.id)
    },
  },

  mounted() {
    this.currentProperty = { ...this.propertyList[0] }
  },
}
</script>

<style lang="scss" scoped>
div,
button {
  font-family: Akrobat;
}
.property-page {
  display: flex;
  justify-content: center;
  margin-top: 18.426vh;
  width: 100%;
  .empty-list {
    width: 100%;
    position: relative;
    padding-top: 11rem;
    font-size: 2.5rem;
    display: flex;
    align-items: center;
    justify-content: center;
    text-align: center;
    &:before {
      content: '';
      width: 7rem;
      height: 7rem;
      position: absolute;
      bottom: -2.5rem;
      transform: translateY(100%);
      background-size: contain;
      background-position: center;
      background-repeat: no-repeat;
      background-image: url('/img/familyMenu/world-grid.svg');
    }
  }
  .property-list {
    display: flex;
    flex-flow: column;
    overflow-y: auto;
    gap: 0.926vh;
    &::-webkit-scrollbar {
      background: rgba(255, 255, 255, 0.1);
      border-radius: 0.093vh;
      width: 0.093vh;
      &-thumb {
        background: rgba(255, 255, 255, 0.6);
        border-radius: 0.093vh;
      }
    }
    &__item {
      display: flex;
      align-items: center;
      justify-content: flex-start;
      padding-left: 2.222vh;
      width: 32.407vh;
      height: 8.148vh;
      text-transform: uppercase;
      color: #ffffff;
      border: 0.093vh solid transparent;
      border-right: none;
      font-style: normal;
      font-weight: 700;
      font-size: 2.222vh;
      line-height: 2.778vh;
      border-image: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.2) 0%,
          rgba(255, 255, 255, 0) 100%
        )
        1 1 round;
      &::before {
        content: '';
        height: 2.517vh;
        width: 0.194vh;
        background: #ffffff;
        margin-right: 1.852vh;
        box-shadow: 0vh 0vh 1.355vh rgba(255, 255, 255, 0.55);
      }
      &:hover,
      &.active {
        background: linear-gradient(
          90deg,
          rgba(255, 255, 255, 0.15) 0%,
          rgba(255, 255, 255, 0) 100%
        );
        transition: 0.2s;
      }
    }
  }

  .center {
    margin-left: 8.611vh;
    .img {
      width: 59.352vh;
      height: 36.19vh;
      background-repeat: no-repeat;
      background-size: cover;
      background-color: rgba(112, 112, 112, 0.103);
    }
    .row {
      display: flex;
      margin-top: 1.852vh;
      gap: 2.778vh;
      align-items: center;
      button {
        display: flex;
        font-weight: 700;
        font-size: 1.852vh;
        line-height: 2.315vh;
        gap: 1.574vh;
        color: #ffffff;
        width: 23.241vh;
        height: 5.833vh;
        img {
          width: 1.481vh;
          height: 1.944vh;
        }
      }
      .title-item {
        margin-top: 1.111vh;
      }
    }
  }

  .left {
    margin-left: 4.815vh;
    .biz-name {
      font-weight: 800;
      font-size: 4.444vh;
      line-height: 4.444vh;
      margin-top: -0.556vh;
      margin-bottom: 0.556vh;
      background: linear-gradient(90deg, #301934  0%, #591b87 100%);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0vh 0vh 10.425vh rgba(255, 255, 255, 0.25);
    }
    .id {
      font-weight: 600;
      font-size: 1.852vh;
      line-height: 2.315vh;
    }
    .stats {
      display: flex;
      flex-direction: column;
      gap: 5.556vh;
      margin-top: 6.389vh;
      .item {
        display: flex;
        gap: 1.759vh;
        position: relative;
        &::before {
          content: '';
          width: 11.111vh;
          height: 11.111vh;
          position: absolute;
          top: 50%;
          left: 0;
          transform: translate(-25%, -50%);
          display: flex;
          background: url(/img/familyMenu/propertyPage/ico-money.svg) no-repeat;
          background-size: cover;
          z-index: -1;
          opacity: 0.05;
        }
        &.item-0::before {
          width: 10vh;
          height: 12.407vh;
          background-image: url(/img/familyMenu/propertyPage/ico-shield.svg);
        }
        &.item-1::before {
          width: 12.685vh;
          height: 13.981vh;
          background-image: url(/img/familyMenu/propertyPage/ico-bag.svg);
        }
        &.item-2::before {
          width: 10vh;
          height: 12.5vh;
          background-image: url(/img/familyMenu/propertyPage/ico-money.svg);
        }
        &:nth-child(1) img {
          width: 4.907vh;
          height: 6.111vh;
        }
        &:nth-child(2) img {
          width: 6.019vh;
          height: 6.667vh;
        }
        &:nth-child(3) img {
          width: 5.185vh;
          height: 7.315vh;
        }
      }
    }
  }
}
</style>
