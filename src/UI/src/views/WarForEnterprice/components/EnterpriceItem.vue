<template>
  <div class="enterprise-item">
    <div
      class="preview"
      :style="{
        backgroundImage: `url(/img/warForEnterprice/enterprices/${
          companiesConfig[item.key].Image
        }.png)`,
      }"
    >
      <div class="date" v-if="item.date">
        <img src="/img/warForEnterprice/time.svg" alt="" />
        {{ item.date }}
      </div>
    </div>
    <div class="about">
      <div class="title">{{ companiesConfig[item.key].Name }}</div>
      <div class="state" v-if="item.captureisInProgress">
        {{ loc('war_for_enterprice_2') }}
      </div>
      <div v-else class="state">
        {{
          item.orgId > 0 ? `${currentOrgName} ` : loc('war_for_enterprice_3')
        }}
      </div>
    </div>
  </div>
</template>

<script>
import { mapGetters, mapState } from 'vuex'
import warcompanies from '../../../configs/families/warcompanies'
export default {
  name: 'EnterpriceItem',
  props: {
    item: Object,
  },
  computed: {
    ...mapState('familyMenu/ratingPage', ['orgList']),
    ...mapState('warForEnterprice', ['fractionNames']),
    ...mapGetters('localization', ['loc']),
    companiesConfig: function() {
      return warcompanies
    },
    currentOrgName() {
      if (this.item.orgId > 0) return this.item.orgName
      return 'Unknown'
    },
  },
}
</script>

<style lang="scss" scoped>
.enterprise-item {
  display: flex;
  flex-direction: column;
  .preview {
    width: 431px;
    height: 262.8px;
    background-size: cover;
    display: flex;
    align-items: flex-end;
    .date {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;
      gap: 11px;
      height: 40px;
      color: #fff;
      background: rgba(0, 0, 0, 0.7);
    }
  }
  .about {
    width: 431px;
    height: 100px;
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    gap: 10px;
    background: rgba(0, 0, 0, 0.4);
    .title {
      font-weight: 800;
      font-size: 24px;
      line-height: 29px;
      text-transform: uppercase;
      color: #fff;
    }
    .state {
      font-weight: 700;
      font-size: 20px;
      line-height: 24px;
      color: rgba(255, 255, 255, 0.5);
      text-transform: uppercase;
    }
  }
}
</style>
