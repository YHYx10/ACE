<template>
  <div class="war-for-enterprice">
    <div class="content">
      <div class="about">
        <div class="title">
        Enterprises<br />
          Captured <br />
          YOU
        </div>
        <div class="item">
          <img src="/img/warForEnterprice/house.png" alt="" />
          <TitleItem
            :name="'Enterprises'"
            :value="enterpricesCount ? [enterpricesCount] : ['NO']"
          />
        </div>
        <div class="item">
          <img src="/img/warForEnterprice/bag.png" alt="" />
          <TitleItem
            :name="'Current profit per minute'"
            :value="[`$${getProfit.toLocaleString('ru')}`]"
            :valueColor="'#A0FF98'"
          />
        </div>
      </div>
      <div class="list">
        <EnterpriceItem
          v-for="item in enterpricesList"
          :key="item.id"
          :item="item"
        />
      </div>
    </div>

    <ExitCross class="exit-cross" />
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import EnterpriceItem from './components/EnterpriceItem'
import ExitCross from '../UI/components/ExitCross.vue'
import TitleItem from './components/TitleItem.vue'
export default {
  name: 'WarForEnterprice',
  components: {
    EnterpriceItem,
    ExitCross,
    TitleItem,
  },
  computed: {
    ...mapState('warForEnterprice', [
      'enterpricesList',
      'owner',
      'ownerType',
      'profit',
    ]),
    ...mapGetters('localization', ['loc']),
    enterpricesCount() {
      return this.enterpricesList.filter(
        (element) =>
          element.orgId === this.owner && element.orgType == this.ownerType
      ).length
    },
    getProfit() {
      return this.profit * this.enterpricesCount * this.getFactor
    },
    getFactor() {
      return this.enterpricesCount >= 3 ? 2 : 1
    },
  },
}
</script>

<style lang="scss" scoped>
.exit-cross {
  position: absolute;
  top: 40px;
  right: 40px;
}

.war-for-enterprice {
  width: 100vw;
  height: 100vh;
  background: linear-gradient(
    72.44deg,
    rgba(19, 20, 21, 0.97) 0%,
    rgba(31, 34, 37, 0.97) 100%
  );
  display: flex;
  align-items: center;
  justify-content: center;
  &::before {
    content: '';
    width: 507px;
    height: 614px;
    position: absolute;
    background: url('/img/warForEnterprice/about.png');
    left: 0;
    top: 50%;
    transform: translate(0, -50%);
  }
  .content {
    display: flex;
    gap: 69px;
    .about {
      display: flex;
      flex-direction: column;
      gap: 45px;
      .title {
        font-weight: 700;
        font-size: 64px;
        line-height: 77px;
        text-transform: uppercase;
        color: #ffffff;
      }
      .item {
        display: flex;
        gap: 24px;
        img {
          width: 62px;
          height: 62px;
        }
      }
    }
    .list {
      width: 1333px;
      height: 744px;
      display: flex;
      gap: 20px;
      flex-wrap: wrap;
      overflow-y: auto;
      &::-webkit-scrollbar {
        width: 0;
      }
    }
  }
}
</style>
