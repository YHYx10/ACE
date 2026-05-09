<template>
  <div class="choice target-choice">
    <HeadItem
      :title="loc('fam:btl:choice:targ')"
      :text="loc('fam:btl:choice:biz')"
    />
    <!-- <div class="target">{{loc('fam:btl:choice:targ')}}</div>
    <div class="desc">{{loc('fam:btl:choice:biz')}}</div> -->
    <div class="target-list">
      <div
        :class="[{ active: item.id === currentBuiss }, 'item']"
        @click="setCurrentBuiss(item.id)"
        v-for="item in businessList"
        :key="item.key"
      >
        <div class="left item-main">
          <div class="id">id: {{ item.id }}</div>
          <div class="value">{{ loc(item.name) }}</div>
        </div>
        <div class="center">
          <TitleItem
            :name="loc('fam:btl:choice:owner')"
            :value="[item.famOwner]"
            :fontSize="24"
          />
        </div>
        <div class="right">
          <TitleItem
            :name="loc('fam:btl:choice:inhour')"
            :value="[`$ ${item.income.toLocaleString('en-US')}`]"
            :fontSize="24"
          />
        </div>

        <!-- <div class="item__owner">
          <div class="desc">{{loc('fam:btl:choice:owner')}}:</div>
          <div class="value">{{item.famOwner}}</div>
        </div> -->
        <!-- <div class="item__income">
          <div class="desc">{{loc('fam:btl:choice:inhour')}}</div>
          <div class="value">${{item.income}}</div>
        </div> -->
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import HeadItem from '../../components/HeadItem.vue'
import TitleItem from '../../../components/TitleItem'
export default {
  name: 'TargetChoice',
  props: {
    setCurrentBuiss: Function,
    currentBuiss: Number,
  },
  computed: {
    ...mapState('familyMenu/eventsPage', ['businessList']),
    ...mapGetters('localization', ['loc']),
  },
  components: { HeadItem, TitleItem },
}
</script>

<style lang="scss" scoped>
.target-choice {
  display: flex;
  flex-flow: column;
  font-family: Akrobat;
  margin-left: 7.87vh;
  .target-list {
    width: 75.556vh;
    margin-top: 2.593vh;
    overflow-y: auto;
    height: 54.167vh;
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
      width: 100%;
      height: 10.093vh;
      position: relative;
      display: flex;
      align-items: center;
      margin-bottom: 0.926vh;
      border: 0.093vh solid rgba(255, 255, 255, 0.09);
      overflow: hidden;
      &::before {
        content: '';
        width: 74.074vh;
        height: 4.815vh;
        bottom: -10.185vh;
        position: absolute;
        transform: translate(0, 100%);
        background: rgba(255, 255, 255, 0.55);
        filter: blur(8.241vh);
        z-index: -1;
      }
      &:hover::before {
        background: rgb(255, 255, 255);
        transform: translate(0, 50%);
      }
      &.active::before {
        background: #ff0000;
        bottom: 0vh;
        transform: translate(0, 50%);
      }

      .left {
        display: flex;
        flex-direction: column;
        margin-left: 3.704vh;
        text-transform: uppercase;
        font-weight: 700;
        font-size: 1.852vh;
        line-height: 2.315vh;
        color: #ffffff;
        .id {
          color: rgba(255, 255, 255, 0.44);
          margin-bottom: 0.741vh;
        }
        .value {
          width: 19.074vh;
        }
      }

      .center {
        width: 19.907vh;
        margin-left: 11.204vh;
      }
      .right {
        margin-left: 6.667vh;
      }
    }
  }
}
</style>
