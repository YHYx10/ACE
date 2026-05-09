<template>
  <div class="management-panel">
    <div class="management-panel__shell">
      <aside class="management-panel__sidebar">
        <div class="management-panel__brand">
          <span class="management-panel__brand-mark">A</span>
          <div>
            <span class="management-panel__eyebrow">ASTRO</span>
            <h1>Management</h1>
          </div>
        </div>

        <button
          v-for="item in navItems"
          :key="item.id"
          class="management-panel__nav-item"
          :class="{ active: activeTab === item.id }"
          type="button"
          @click="activeTab = item.id"
        >
          <span class="management-panel__nav-icon">{{ item.icon }}</span>
          <span>{{ item.label }}</span>
        </button>

        <div class="management-panel__session">
          <span class="management-panel__status-dot"></span>
          <div>
            <strong>{{ dashboard.managementAccess }}</strong>
            <small>Admin authentication required</small>
          </div>
        </div>
      </aside>

      <main class="management-panel__content">
        <header class="management-panel__header">
          <div>
            <span class="management-panel__eyebrow">Senior Staff Only</span>
            <h2>{{ activeItem.title }}</h2>
            <p>{{ activeItem.description }}</p>
          </div>

          <div class="management-panel__header-actions">
            <button class="management-panel__ghost" type="button" @click="refreshData">
              REFRESH
            </button>
            <button class="management-panel__close" type="button" @click="closePanel">
              CLOSE
            </button>
          </div>
        </header>

        <section class="management-panel__hero">
          <div class="management-panel__hero-copy">
            <span class="management-panel__status-dot"></span>
            <span>{{ heroStatus }}</span>
          </div>
          <strong>{{ loadedAt || 'WAITING FOR DATA' }}</strong>
        </section>

        <section v-if="error" class="management-panel__error">
          {{ error }}
        </section>

        <section v-if="loading" class="management-panel__loading">
          Loading management data...
        </section>

        <template v-else>
          <section v-if="activeTab === 'dashboard'" class="management-panel__grid">
            <article
              v-for="card in dashboardCards"
              :key="card.label"
              class="management-panel__card"
            >
              <span>{{ card.label }}</span>
              <strong>{{ card.value }}</strong>
              <p>{{ card.text }}</p>
            </article>
          </section>

          <section v-if="activeTab === 'players'" class="management-panel__players">
            <div class="management-panel__section-title">
              <strong>Online Players</strong>
              <span>Safe actions only: goto, bring, freeze, unfreeze, spectate</span>
            </div>

            <div class="management-panel__search">
              <input
                v-model.trim="playerSearch"
                type="text"
                placeholder="Search by player name or character UUID..."
              >
              <span>{{ filteredPlayers.length }} / {{ players.length }} online</span>
            </div>

            <div v-if="!filteredPlayers.length" class="management-panel__empty">
              No online players matched your search.
            </div>

            <article
              v-for="player in filteredPlayers"
              :key="player.uuid"
              class="management-panel__player-row"
            >
              <div class="management-panel__staff-id">
                <strong>{{ player.name }}</strong>
                <span>UUID {{ player.uuid }}</span>
              </div>
              <div>
                <small>ADMIN</small>
                <strong>{{ player.adminLevel }}</strong>
              </div>
              <div>
                <small>FACTION</small>
                <strong>{{ player.faction }}</strong>
              </div>
              <div>
                <small>PING</small>
                <strong>{{ player.ping }} ms</strong>
              </div>
              <div>
                <small>STATUS</small>
                <strong class="is-online">{{ player.status }}</strong>
              </div>
              <div class="management-panel__actions">
                <button type="button" @click="runPlayerAction('goto', player)">Goto</button>
                <button type="button" @click="runPlayerAction('bring', player)">Bring</button>
                <button type="button" @click="runPlayerAction('freeze', player)">Freeze</button>
                <button type="button" @click="runPlayerAction('unfreeze', player)">Unfreeze</button>
                <button type="button" @click="runPlayerAction('spectate', player)">Spectate</button>
              </div>
            </article>
          </section>

          <section v-if="activeTab === 'punishments'" class="management-panel__punishments">
            <div class="management-panel__section-title">
              <strong>Punishments / Discipline</strong>
              <span>Reason required. Existing ASTRO moderation systems only.</span>
            </div>

            <div class="management-panel__punishment-layout">
              <div class="management-panel__punishment-panel">
                <div class="management-panel__section-title management-panel__section-title--compact">
                  <strong>Select Player</strong>
                  <span>{{ punishmentPlayers.length }} online targets</span>
                </div>

                <div class="management-panel__search">
                  <input
                    v-model.trim="punishmentSearch"
                    type="text"
                    placeholder="Search online player by name or UUID..."
                  >
                </div>

                <div v-if="!punishmentPlayers.length" class="management-panel__empty">
                  No online players matched your search.
                </div>

                <button
                  v-for="player in punishmentPlayers"
                  :key="player.uuid"
                  class="management-panel__target-row"
                  :class="{ active: selectedPunishmentPlayerUuid === player.uuid }"
                  type="button"
                  @click="selectPunishmentPlayer(player)"
                >
                  <span>
                    <strong>{{ player.name }}</strong>
                    <small>UUID {{ player.uuid }} / ADMIN {{ player.adminLevel }}</small>
                  </span>
                  <em>{{ player.status }}</em>
                </button>
              </div>

              <div class="management-panel__punishment-panel">
                <div class="management-panel__section-title management-panel__section-title--compact">
                  <strong>Action</strong>
                  <span>{{ selectedPunishmentPlayer ? selectedPunishmentPlayer.name : 'No player selected' }}</span>
                </div>

                <div class="management-panel__punishment-types">
                  <button
                    v-for="type in punishmentTypes"
                    :key="type.id"
                    type="button"
                    :class="{ active: punishmentType === type.id }"
                    @click="punishmentType = type.id"
                  >
                    <strong>{{ type.label }}</strong>
                    <small>{{ type.helper }}</small>
                  </button>
                </div>

                <label
                  v-if="punishmentRequiresDuration"
                  class="management-panel__field"
                >
                  <span>Duration {{ punishmentDurationUnit }}</span>
                  <input
                    v-model.number="punishmentDuration"
                    min="1"
                    type="number"
                  >
                </label>

                <label class="management-panel__field">
                  <span>Reason</span>
                  <textarea
                    v-model.trim="punishmentReason"
                    maxlength="180"
                    placeholder="Required. Explain why this discipline action is being issued..."
                  ></textarea>
                </label>

                <button
                  class="management-panel__execute"
                  type="button"
                  :disabled="!canReviewPunishment"
                  @click="openPunishmentConfirm"
                >
                  Review Action
                </button>
              </div>

              <div class="management-panel__punishment-panel management-panel__punishment-panel--history">
                <div class="management-panel__section-title management-panel__section-title--compact">
                  <strong>Punishment History</strong>
                  <span>{{ punishmentHistoryTargetLabel }}</span>
                </div>

                <div v-if="punishmentHistoryError" class="management-panel__error management-panel__error--compact">
                  {{ punishmentHistoryError }}
                </div>

                <div v-else-if="!selectedPunishmentPlayerUuid" class="management-panel__empty">
                  Select an online player to load history.
                </div>

                <div v-else-if="!punishmentHistory.length" class="management-panel__empty">
                  No existing punishment history found for this player.
                </div>

                <div v-else class="management-panel__history-list">
                  <article
                    v-for="(entry, index) in punishmentHistory"
                    :key="`${entry.time}-${entry.action}-${index}`"
                    class="management-panel__history-row"
                  >
                    <div>
                      <strong>{{ entry.action }}</strong>
                      <span>{{ entry.source }} / {{ entry.time || '-' }}</span>
                    </div>
                    <small>{{ entry.admin || 'Unknown admin' }}</small>
                  </article>
                </div>

                <p v-if="punishmentHistoryMessage" class="management-panel__history-note">
                  {{ punishmentHistoryMessage }}
                </p>
              </div>
            </div>
          </section>

          <section v-if="activeTab === 'database'" class="management-panel__database">
            <div class="management-panel__section-title">
              <strong>Database / Characters</strong>
              <span>Read-only character search and profile inspection</span>
            </div>

            <div class="management-panel__database-layout">
              <div class="management-panel__database-panel">
                <div class="management-panel__section-title management-panel__section-title--compact">
                  <strong>Character Search</strong>
                  <span>{{ databaseSearchResults.length }} results</span>
                </div>

                <div class="management-panel__search">
                  <input
                    v-model.trim="databaseSearch"
                    type="text"
                    placeholder="UUID, first name, last name, phone, account ID or login..."
                    @keyup.enter="searchCharacters"
                  >
                  <button type="button" @click="searchCharacters">Search</button>
                </div>

                <div v-if="databaseError" class="management-panel__error management-panel__error--compact">
                  {{ databaseError }}
                </div>

                <div v-if="!databaseSearchResults.length" class="management-panel__empty">
                  Search for a character or press Search to load recent records.
                </div>

                <button
                  v-for="character in databaseSearchResults"
                  :key="character.uuid"
                  class="management-panel__database-result"
                  :class="{ active: databaseProfileData && databaseProfileData.uuid === character.uuid }"
                  type="button"
                  @click="loadCharacterProfile(character.uuid)"
                >
                  <div>
                    <strong>{{ character.name }}</strong>
                    <span>UUID {{ character.uuid }} / ACC {{ character.accountId || '-' }}</span>
                  </div>
                  <div>
                    <small>{{ character.online ? 'ONLINE' : 'OFFLINE' }}</small>
                    <em>LVL {{ character.level }} / ADM {{ character.adminLevel }}</em>
                  </div>
                </button>

                <p v-if="databaseSearchMessage" class="management-panel__history-note">
                  {{ databaseSearchMessage }}
                </p>
              </div>

              <div class="management-panel__database-panel management-panel__database-panel--profile">
                <div v-if="!databaseProfileData" class="management-panel__empty">
                  Select a character to inspect profile data.
                </div>

                <template v-else>
                  <div class="management-panel__profile-head">
                    <div>
                      <span class="management-panel__eyebrow">Character Profile</span>
                      <h3>{{ databaseProfileData.name }}</h3>
                      <p>UUID {{ databaseProfileData.uuid }} / {{ databaseProfileData.status }}</p>
                    </div>
                    <strong :class="databaseProfileData.online ? 'is-online' : 'is-offline'">
                      {{ databaseProfileData.status }}
                    </strong>
                  </div>

                  <div class="management-panel__profile-grid">
                    <article v-for="item in databaseProfileStats" :key="item.label">
                      <span>{{ item.label }}</span>
                      <strong>{{ item.value }}</strong>
                    </article>
                  </div>

                  <div class="management-panel__related-grid">
                    <section>
                      <h4>Identity</h4>
                      <div class="management-panel__detail-list">
                        <span>First Name <strong>{{ databaseProfileData.firstName }}</strong></span>
                        <span>Last Name <strong>{{ databaseProfileData.lastName }}</strong></span>
                        <span>Phone <strong>{{ databaseProfileData.phone || '-' }}</strong></span>
                        <span>Account <strong>{{ databaseProfileData.accountLogin || databaseProfileData.accountId || '-' }}</strong></span>
                        <span>Created <strong>{{ databaseProfileData.createdAt || '-' }}</strong></span>
                      </div>
                    </section>

                    <section>
                      <h4>Faction / Family</h4>
                      <div class="management-panel__detail-list">
                        <span>Faction <strong>{{ databaseProfileData.faction }}</strong></span>
                        <span>Faction Rank <strong>{{ databaseProfileData.factionLevel }}</strong></span>
                        <span>Family <strong>{{ databaseProfileData.family }}</strong></span>
                        <span>Family Rank <strong>{{ databaseProfileData.familyLevel }}</strong></span>
                      </div>
                    </section>
                  </div>

                  <div class="management-panel__related-grid management-panel__related-grid--wide">
                    <section>
                      <h4>Vehicles</h4>
                      <div v-if="!databaseRelated.vehicles.length" class="management-panel__mini-empty">No owned vehicles found.</div>
                      <article v-for="vehicle in databaseRelated.vehicles" :key="vehicle.id" class="management-panel__related-row">
                        <strong>{{ vehicle.name || vehicle.model }}</strong>
                        <span>#{{ vehicle.id }} / {{ vehicle.number }} / {{ vehicle.status }}</span>
                      </article>
                    </section>

                    <section>
                      <h4>Licenses</h4>
                      <div v-if="!databaseProfileData.licenses.length" class="management-panel__mini-empty">No active license data found.</div>
                      <article v-for="(license, index) in databaseProfileData.licenses" :key="`${license.name}-${index}`" class="management-panel__related-row">
                        <strong>{{ license.name }}</strong>
                        <span>{{ license.status }} {{ license.expires ? `/ ${license.expires}` : '' }}</span>
                      </article>
                    </section>

                    <section>
                      <h4>Properties</h4>
                      <div v-if="!databaseRelated.houses.length && !databaseRelated.businesses.length" class="management-panel__mini-empty">No houses or businesses found.</div>
                      <article v-for="house in databaseRelated.houses" :key="`house-${house.id}`" class="management-panel__related-row">
                        <strong>House #{{ house.id }}</strong>
                        <span>{{ house.name }} / ${{ house.price }}</span>
                      </article>
                      <article v-for="business in databaseRelated.businesses" :key="`business-${business.id}`" class="management-panel__related-row">
                        <strong>{{ business.name || `Business #${business.id}` }}</strong>
                        <span>{{ business.type }} / ${{ business.price }}</span>
                      </article>
                    </section>

                    <section>
                      <h4>Punishment / Ban History</h4>
                      <div v-if="databaseRelated.activeBan" class="management-panel__active-ban">
                        Active ban: {{ databaseRelated.activeBan.reason }} / until {{ databaseRelated.activeBan.until }}
                      </div>
                      <div v-if="!databaseRelated.punishmentHistory.length" class="management-panel__mini-empty">No existing punishment history found.</div>
                      <article
                        v-for="(entry, index) in databaseRelated.punishmentHistory"
                        :key="`${entry.time}-${entry.action}-${index}`"
                        class="management-panel__related-row"
                      >
                        <strong>{{ entry.action }}</strong>
                        <span>{{ entry.admin || 'Unknown admin' }} / {{ entry.time || '-' }}</span>
                      </article>
                    </section>
                  </div>
                </template>
              </div>
            </div>
          </section>

          <section v-if="activeTab === 'staff'" class="management-panel__staff">
            <div class="management-panel__section-title">
              <strong>Senior Staff Registry</strong>
              <span>characters.adminlvl 8-10, linked with admin_users char:&lt;uuid&gt;</span>
            </div>

            <div v-if="!seniorStaff.length" class="management-panel__empty">
              No senior staff records found.
            </div>

            <article
              v-for="staff in seniorStaff"
              :key="staff.uuid"
              class="management-panel__staff-row"
            >
              <div class="management-panel__staff-id">
                <strong>{{ staff.name }}</strong>
                <span>UUID {{ staff.uuid }}</span>
              </div>
              <div>
                <small>ADMIN LEVEL</small>
                <strong>{{ staff.adminLevel }} / {{ staff.rankName }}</strong>
              </div>
              <div>
                <small>SESSION</small>
                <strong :class="staff.authenticated ? 'is-online' : staff.online ? 'is-warn' : 'is-offline'">
                  {{ staff.sessionStatus }}
                </strong>
              </div>
              <div>
                <small>LAST LOGIN</small>
                <strong>{{ staff.lastLogin }}</strong>
              </div>
              <div>
                <small>LAST SEEN</small>
                <strong>{{ staff.lastSeen }}</strong>
              </div>
              <div>
                <small>ACCOUNT</small>
                <strong :class="staff.adminAccountActive ? 'is-online' : 'is-offline'">
                  {{ staff.adminAccountActive ? 'ACTIVE' : 'NOT REGISTERED' }}
                </strong>
              </div>
            </article>
          </section>

          <section v-if="activeTab === 'server'" class="management-panel__server">
            <div class="management-panel__grid management-panel__grid--compact">
              <article
                v-for="card in serverCards"
                :key="card.label"
                class="management-panel__card"
              >
                <span>{{ card.label }}</span>
                <strong>{{ card.value }}</strong>
                <p>{{ card.text }}</p>
              </article>
            </div>

            <div class="management-panel__notice">
              <strong>Dangerous controls disabled</strong>
              <span>Restart, stop, console and resource actions are intentionally not available in this read-only phase.</span>
            </div>

            <div class="management-panel__notes">
              <div v-for="note in serverNotes" :key="note">
                <span class="management-panel__status-dot"></span>
                {{ note }}
              </div>
            </div>
          </section>
        </template>
      </main>

      <div
        v-if="pendingPunishment"
        class="management-panel__modal"
      >
        <div class="management-panel__modal-card">
          <span class="management-panel__eyebrow">Confirm Discipline Action</span>
          <h3>{{ pendingPunishment.label }} {{ pendingPunishment.targetName }}</h3>
          <p>
            This action will run through the existing ASTRO moderation backend and will be logged.
          </p>
          <dl>
            <div>
              <dt>Target</dt>
              <dd>{{ pendingPunishment.targetName }} / UUID {{ pendingPunishment.targetUuid }}</dd>
            </div>
            <div>
              <dt>Action</dt>
              <dd>{{ pendingPunishment.label }}</dd>
            </div>
            <div v-if="pendingPunishment.duration > 0">
              <dt>Duration</dt>
              <dd>{{ pendingPunishment.duration }} {{ punishmentDurationUnit }}</dd>
            </div>
            <div>
              <dt>Reason</dt>
              <dd>{{ pendingPunishment.reason }}</dd>
            </div>
          </dl>
          <div class="management-panel__modal-actions">
            <button type="button" @click="cancelPunishment">Cancel</button>
            <button type="button" class="is-danger" @click="confirmPunishment">Confirm</button>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'ManagementPanel',
  data() {
    return {
      activeTab: 'dashboard',
      navItems: [
        {
          id: 'dashboard',
          icon: '01',
          label: 'Dashboard',
          title: 'Management Dashboard',
          description: 'Read-only live overview for authenticated senior management.'
        },
        {
          id: 'players',
          icon: '02',
          label: 'Players',
          title: 'Player Management',
          description: 'Online player search and controlled senior-staff actions.'
        },
        {
          id: 'punishments',
          icon: '03',
          label: 'Punishments',
          title: 'Punishments / Discipline',
          description: 'Controlled kick, warn, mute, jail and ban actions through existing ASTRO moderation systems.'
        },
        {
          id: 'database',
          icon: '04',
          label: 'Database',
          title: 'Database / Characters',
          description: 'Read-only character lookup, profile details and related records for senior management.'
        },
        {
          id: 'staff',
          icon: '05',
          label: 'Senior Staff',
          title: 'Senior Staff Overview',
          description: 'Senior admin registry and live /alogin session state.'
        },
        {
          id: 'server',
          icon: '06',
          label: 'Server Control',
          title: 'Server Control Overview',
          description: 'Safe read-only server status. Dangerous controls are disabled.'
        }
      ],
      playerSearch: '',
      punishmentSearch: '',
      selectedPunishmentPlayerUuid: 0,
      punishmentType: 'kick',
      punishmentDuration: 30,
      punishmentReason: '',
      pendingPunishment: null,
      databaseSearch: '',
      punishmentTypes: [
        { id: 'kick', label: 'Kick', helper: 'Disconnect selected player.', duration: false },
        { id: 'warn', label: 'Warn', helper: 'Issue an existing ASTRO warning.', duration: false },
        { id: 'mute', label: 'Mute', helper: 'Mute player chat/voice temporarily.', duration: true, unit: 'minutes' },
        { id: 'unmute', label: 'Unmute', helper: 'Remove active mute if present.', duration: false },
        { id: 'jail', label: 'Jail', helper: 'Send player to Demorgan/jail.', duration: true, unit: 'minutes' },
        { id: 'ban', label: 'Ban', helper: 'Use existing ASTRO ban system.', duration: true, unit: 'days' }
      ]
    };
  },
  computed: {
    state() {
      return this.$store.state.managementPanel || {};
    },
    loading() {
      return !!this.state.loading;
    },
    error() {
      return this.state.error || '';
    },
    loadedAt() {
      return this.state.loadedAt || '';
    },
    dashboard() {
      return this.state.dashboard || {};
    },
    seniorStaff() {
      return Array.isArray(this.state.seniorStaff) ? this.state.seniorStaff : [];
    },
    players() {
      return Array.isArray(this.state.players) ? this.state.players : [];
    },
    filteredPlayers() {
      const query = this.playerSearch.toLowerCase();
      if (!query) return this.players;

      return this.players.filter(player => {
        const name = String(player.name || '').toLowerCase();
        const uuid = String(player.uuid || '').toLowerCase();
        return name.includes(query) || uuid.includes(query);
      });
    },
    punishmentPlayers() {
      const query = this.punishmentSearch.toLowerCase();
      const source = this.players.filter(player => player && player.uuid);
      if (!query) return source;

      return source.filter(player => {
        const name = String(player.name || '').toLowerCase();
        const uuid = String(player.uuid || '').toLowerCase();
        return name.includes(query) || uuid.includes(query);
      });
    },
    selectedPunishmentPlayer() {
      return this.players.find(player => player.uuid === this.selectedPunishmentPlayerUuid) || null;
    },
    selectedPunishmentType() {
      return this.punishmentTypes.find(type => type.id === this.punishmentType) || this.punishmentTypes[0];
    },
    punishmentRequiresDuration() {
      return !!(this.selectedPunishmentType && this.selectedPunishmentType.duration);
    },
    punishmentDurationUnit() {
      return (this.selectedPunishmentType && this.selectedPunishmentType.unit) || 'minutes';
    },
    canReviewPunishment() {
      if (!this.selectedPunishmentPlayer) return false;
      if (!this.punishmentReason || this.punishmentReason.length < 3) return false;
      if (this.punishmentRequiresDuration && (!this.punishmentDuration || this.punishmentDuration <= 0)) return false;
      return true;
    },
    punishmentHistory() {
      return Array.isArray(this.state.punishmentHistory) ? this.state.punishmentHistory : [];
    },
    punishmentHistoryTargetLabel() {
      const target = this.state.punishmentHistoryTarget;
      if (target && target.name) return target.name;
      return this.selectedPunishmentPlayer ? this.selectedPunishmentPlayer.name : 'No target selected';
    },
    punishmentHistoryMessage() {
      return this.state.punishmentHistoryMessage || '';
    },
    punishmentHistoryError() {
      return this.state.punishmentHistoryError || '';
    },
    databaseSearchResults() {
      return Array.isArray(this.state.databaseSearchResults) ? this.state.databaseSearchResults : [];
    },
    databaseSearchMessage() {
      return this.state.databaseSearchMessage || '';
    },
    databaseProfile() {
      return this.state.databaseProfile || null;
    },
    databaseProfileData() {
      return this.databaseProfile && this.databaseProfile.profile ? this.databaseProfile.profile : null;
    },
    databaseRelated() {
      const related = this.databaseProfile && this.databaseProfile.related ? this.databaseProfile.related : {};
      return {
        vehicles: Array.isArray(related.vehicles) ? related.vehicles : [],
        houses: Array.isArray(related.houses) ? related.houses : [],
        businesses: Array.isArray(related.businesses) ? related.businesses : [],
        punishmentHistory: Array.isArray(related.punishmentHistory) ? related.punishmentHistory : [],
        activeBan: related.activeBan || null,
        notes: Array.isArray(related.notes) ? related.notes : []
      };
    },
    databaseError() {
      return this.state.databaseError || '';
    },
    databaseProfileStats() {
      const profile = this.databaseProfileData || {};
      const wanted = profile.wanted || {};
      return [
        { label: 'Cash', value: `$${profile.money || 0}` },
        { label: 'Bank', value: `$${profile.bank || 0}` },
        { label: 'Warnings', value: profile.warnings || 0 },
        { label: 'Wanted', value: wanted.level || 0 },
        { label: 'Fines', value: `$${profile.fines || 0}` },
        { label: 'Admin Level', value: profile.adminLevel || 0 }
      ];
    },
    server() {
      return this.state.server || {};
    },
    activeItem() {
      return this.navItems.find(item => item.id === this.activeTab) || this.navItems[0];
    },
    heroStatus() {
      const requester = this.dashboard.requester;
      if (!requester) return 'Secure management data channel ready';
      return `${requester.name} / UUID ${requester.uuid} / ADMIN LEVEL ${requester.adminLevel}`;
    },
    dashboardCards() {
      return [
        { label: 'ONLINE PLAYERS', value: this.dashboard.onlinePlayers || 0, text: 'Logged-in players currently visible to the server.' },
        { label: 'AUTH ADMINS', value: this.dashboard.authenticatedAdmins || 0, text: 'Admins currently authenticated through /alogin.' },
        { label: 'OPEN REPORTS', value: this.dashboard.openReports || 0, text: 'Live open report count from the report manager.' },
        { label: 'UNASSIGNED REPORTS', value: this.dashboard.unassignedReports || 0, text: 'Open reports without an assigned/answering admin.' },
        { label: 'TOTAL ADMINS', value: this.dashboard.totalAdmins || 0, text: 'Characters with admin level 1 or higher.' },
        { label: 'SENIOR STAFF', value: this.dashboard.totalSeniorStaff || 0, text: 'Characters with admin level 8, 9, or 10.' }
      ];
    },
    serverCards() {
      return [
        { label: 'RESOURCE', value: this.server.resource || 'Whistler', text: 'Primary ASTRO server resource responding to management requests.' },
        { label: 'STATUS', value: this.server.status || 'UNKNOWN', text: 'Read-only status returned by the management service.' },
        { label: 'PLAYERS', value: this.server.onlinePlayers || 0, text: 'Current logged-in player count.' },
        { label: 'REPORTS', value: this.server.openReports || 0, text: 'Current open reports.' },
        { label: 'UNASSIGNED', value: this.server.unassignedReports || 0, text: 'Reports still waiting for staff ownership.' },
        { label: 'CONTROL MODE', value: this.server.dangerousControls || 'DISABLED', text: 'No dangerous server actions are enabled in Phase 2.' }
      ];
    },
    serverNotes() {
      return Array.isArray(this.server.notes) ? this.server.notes : [];
    }
  },
  mounted() {
    this.refreshData();
  },
  methods: {
    refreshData() {
      this.$store.commit('managementPanel/setLoading', true);
      if (window.mp && window.mp.trigger) {
        window.mp.trigger('management:refresh');
      }
    },
    runPlayerAction(action, player) {
      if (!player || !player.uuid || !window.mp || !window.mp.trigger) return;
      window.mp.trigger('management:playerAction', action, player.uuid);
    },
    selectPunishmentPlayer(player) {
      if (!player || !player.uuid) return;
      this.selectedPunishmentPlayerUuid = player.uuid;
      this.requestPunishmentHistory(player.uuid);
    },
    requestPunishmentHistory(uuid) {
      if (!uuid || !window.mp || !window.mp.trigger) return;
      window.mp.trigger('management:punishment:requestHistory', uuid);
    },
    openPunishmentConfirm() {
      if (!this.canReviewPunishment) return;
      this.pendingPunishment = {
        action: this.punishmentType,
        label: this.selectedPunishmentType.label,
        targetUuid: this.selectedPunishmentPlayer.uuid,
        targetName: this.selectedPunishmentPlayer.name,
        duration: this.punishmentRequiresDuration ? Number(this.punishmentDuration) : 0,
        reason: this.punishmentReason
      };
    },
    cancelPunishment() {
      this.pendingPunishment = null;
    },
    confirmPunishment() {
      if (!this.pendingPunishment || !window.mp || !window.mp.trigger) return;
      window.mp.trigger(
        'management:punishment:execute',
        this.pendingPunishment.action,
        this.pendingPunishment.targetUuid,
        this.pendingPunishment.duration,
        this.pendingPunishment.reason
      );
      this.pendingPunishment = null;
    },
    searchCharacters() {
      if (!window.mp || !window.mp.trigger) return;
      window.mp.trigger('management:database:searchCharacters', this.databaseSearch || '');
    },
    loadCharacterProfile(uuid) {
      if (!uuid || !window.mp || !window.mp.trigger) return;
      window.mp.trigger('management:database:getCharacterProfile', uuid);
    },
    closePanel() {
      if (window.close) window.close();
    }
  }
};
</script>

<style lang="scss" scoped>
.management-panel {
  width: 100vw;
  height: 100vh;
  padding: 4.2vh 4.5vw;
  box-sizing: border-box;
  color: #eef7ff;
  font-family: "Akrobat", "Arial", sans-serif;
  background:
    radial-gradient(circle at 78% 14%, rgba(46, 132, 255, 0.22), transparent 30%),
    radial-gradient(circle at 20% 80%, rgba(8, 244, 206, 0.11), transparent 34%),
    linear-gradient(135deg, rgba(2, 7, 18, 0.94), rgba(3, 18, 37, 0.94));
}

.management-panel__shell {
  display: grid;
  grid-template-columns: 17.5rem 1fr;
  width: 100%;
  height: 100%;
  border: 1px solid rgba(112, 201, 255, 0.22);
  box-shadow: 0 0 3.4rem rgba(0, 174, 255, 0.16), inset 0 0 3rem rgba(30, 87, 144, 0.12);
  background: rgba(2, 10, 22, 0.62);
}

.management-panel__sidebar {
  display: flex;
  flex-direction: column;
  padding: 2.1rem 1.25rem;
  border-right: 1px solid rgba(112, 201, 255, 0.18);
  background: linear-gradient(180deg, rgba(7, 22, 43, 0.88), rgba(3, 11, 24, 0.72));
}

.management-panel__brand {
  display: flex;
  align-items: center;
  gap: 0.9rem;
  margin-bottom: 2.5rem;
}

.management-panel__brand-mark {
  display: grid;
  place-items: center;
  width: 3rem;
  height: 3rem;
  color: #001527;
  font-size: 1.35rem;
  font-weight: 900;
  background: linear-gradient(135deg, #27d8ff, #6cffd8);
  box-shadow: 0 0 1.5rem rgba(40, 216, 255, 0.35);
}

.management-panel__eyebrow {
  display: block;
  color: #63ccff;
  font-size: 0.78rem;
  font-weight: 800;
  letter-spacing: 0.16em;
  text-transform: uppercase;
}

.management-panel h1,
.management-panel h2 {
  margin: 0;
  text-transform: uppercase;
}

.management-panel h1 {
  font-size: 1.35rem;
}

.management-panel h2 {
  margin-top: 0.28rem;
  font-size: clamp(2.1rem, 3.4vw, 4.1rem);
  letter-spacing: 0.03em;
}

.management-panel__nav-item {
  display: flex;
  align-items: center;
  width: 100%;
  gap: 0.85rem;
  margin-bottom: 0.72rem;
  padding: 0.95rem 1rem;
  border: 1px solid rgba(118, 191, 255, 0.14);
  color: rgba(238, 247, 255, 0.76);
  background: rgba(12, 35, 62, 0.45);
  font: inherit;
  font-size: 0.95rem;
  font-weight: 800;
  letter-spacing: 0.04em;
  text-align: left;
  text-transform: uppercase;
  cursor: pointer;
  transition: 0.18s ease;
}

.management-panel__nav-item:hover,
.management-panel__nav-item.active {
  color: #fff;
  border-color: rgba(95, 212, 255, 0.6);
  background: linear-gradient(90deg, rgba(29, 118, 226, 0.52), rgba(22, 238, 211, 0.14));
  box-shadow: inset 0.22rem 0 0 #34d8ff, 0 0 1.15rem rgba(47, 190, 255, 0.15);
}

.management-panel__nav-icon {
  color: #45e4ff;
  font-size: 0.78rem;
  letter-spacing: 0.1em;
}

.management-panel__session {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  margin-top: auto;
  padding: 1rem;
  border: 1px solid rgba(112, 201, 255, 0.18);
  background: rgba(10, 32, 56, 0.38);
}

.management-panel__session strong,
.management-panel__session small {
  display: block;
}

.management-panel__session small {
  margin-top: 0.2rem;
  color: rgba(238, 247, 255, 0.52);
  font-size: 0.78rem;
  text-transform: uppercase;
}

.management-panel__content {
  display: flex;
  flex-direction: column;
  gap: 1.35rem;
  min-width: 0;
  padding: 2.25rem 2.65rem;
  overflow: hidden;
}

.management-panel__header {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 2rem;
}

.management-panel__header p {
  max-width: 46rem;
  margin: 0.65rem 0 0;
  color: rgba(238, 247, 255, 0.62);
  font-size: 1.05rem;
  letter-spacing: 0.035em;
}

.management-panel__header-actions {
  display: flex;
  gap: 0.75rem;
}

.management-panel__close,
.management-panel__ghost {
  padding: 0.72rem 1.65rem;
  border: 1px solid rgba(255, 255, 255, 0.28);
  color: #fff;
  background: rgba(255, 255, 255, 0.06);
  font: inherit;
  font-size: 0.82rem;
  font-weight: 900;
  letter-spacing: 0.13em;
  cursor: pointer;
  transition: 0.18s ease;
}

.management-panel__ghost:hover {
  border-color: rgba(95, 212, 255, 0.8);
  background: rgba(40, 169, 255, 0.18);
  box-shadow: 0 0 1rem rgba(52, 195, 255, 0.2);
}

.management-panel__close:hover {
  border-color: rgba(255, 92, 92, 0.8);
  background: rgba(255, 62, 62, 0.18);
  box-shadow: 0 0 1rem rgba(255, 52, 52, 0.2);
}

.management-panel__hero,
.management-panel__notice,
.management-panel__error,
.management-panel__loading,
.management-panel__empty {
  display: flex;
  align-items: center;
  justify-content: space-between;
  padding: 1.1rem 1.3rem;
  border: 1px solid rgba(106, 218, 255, 0.2);
  background: linear-gradient(90deg, rgba(29, 118, 226, 0.22), rgba(2, 16, 34, 0.4));
}

.management-panel__error {
  border-color: rgba(255, 70, 70, 0.36);
  color: #ffb0b0;
  background: rgba(76, 7, 12, 0.34);
}

.management-panel__loading,
.management-panel__empty {
  justify-content: center;
  color: rgba(238, 247, 255, 0.72);
  text-transform: uppercase;
  letter-spacing: 0.12em;
}

.management-panel__hero-copy {
  display: flex;
  align-items: center;
  gap: 0.65rem;
  color: rgba(238, 247, 255, 0.72);
  font-size: 0.92rem;
  font-weight: 800;
  letter-spacing: 0.1em;
  text-transform: uppercase;
}

.management-panel__hero strong {
  color: #63e7ff;
  font-size: 1rem;
  letter-spacing: 0.12em;
}

.management-panel__status-dot {
  flex: 0 0 auto;
  width: 0.55rem;
  height: 0.55rem;
  border-radius: 50%;
  background: #48ffd4;
  box-shadow: 0 0 1rem rgba(72, 255, 212, 0.75);
}

.management-panel__grid {
  display: grid;
  grid-template-columns: repeat(3, minmax(0, 1fr));
  gap: 1rem;
}

.management-panel__grid--compact .management-panel__card {
  min-height: 10.5rem;
}

.management-panel__card {
  min-height: 12rem;
  padding: 1.25rem;
  border: 1px solid rgba(112, 201, 255, 0.18);
  background:
    linear-gradient(180deg, rgba(20, 61, 104, 0.44), rgba(5, 15, 30, 0.58)),
    repeating-linear-gradient(0deg, rgba(255,255,255,0.025) 0, rgba(255,255,255,0.025) 1px, transparent 1px, transparent 5px);
  box-shadow: inset 0 0 1.5rem rgba(53, 149, 255, 0.08);
}

.management-panel__card span,
.management-panel__staff-row small,
.management-panel__player-row small,
.management-panel__section-title span {
  color: #4fe3ff;
  font-size: 0.73rem;
  font-weight: 900;
  letter-spacing: 0.18em;
  text-transform: uppercase;
}

.management-panel__card strong {
  display: block;
  margin-top: 0.75rem;
  color: #fff;
  font-size: 2rem;
  text-transform: uppercase;
}

.management-panel__card p {
  margin: 0.8rem 0 0;
  color: rgba(238, 247, 255, 0.64);
  font-size: 0.96rem;
  line-height: 1.45;
}

.management-panel__staff,
.management-panel__players,
.management-panel__punishments,
.management-panel__database,
.management-panel__server {
  min-height: 0;
  overflow-y: auto;
  padding-right: 0.3rem;
}

.management-panel__section-title {
  display: flex;
  align-items: flex-end;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
}

.management-panel__section-title--compact {
  margin-bottom: 0.75rem;
}

.management-panel__section-title strong {
  color: #fff;
  font-size: 1.25rem;
  text-transform: uppercase;
}

.management-panel__staff-row {
  display: grid;
  grid-template-columns: 1.3fr 1.1fr 1.05fr 1fr 1fr 0.9fr;
  gap: 1rem;
  align-items: center;
  margin-bottom: 0.72rem;
  padding: 1rem 1.15rem;
  border: 1px solid rgba(112, 201, 255, 0.16);
  background: rgba(4, 18, 36, 0.48);
  box-shadow: inset 0 0 1rem rgba(53, 149, 255, 0.05);
}

.management-panel__search {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
  padding: 0.85rem 1rem;
  border: 1px solid rgba(112, 201, 255, 0.16);
  background: rgba(4, 18, 36, 0.42);
}

.management-panel__search input {
  flex: 1;
  min-width: 0;
  border: 0;
  outline: 0;
  color: #fff;
  background: transparent;
  font: inherit;
  font-size: 0.96rem;
  letter-spacing: 0.05em;
}

.management-panel__search input::placeholder {
  color: rgba(238, 247, 255, 0.36);
}

.management-panel__search span,
.management-panel__search button {
  color: #4fe3ff;
  font-size: 0.78rem;
  font-weight: 900;
  letter-spacing: 0.16em;
  text-transform: uppercase;
}

.management-panel__search button {
  border: 1px solid rgba(112, 201, 255, 0.24);
  padding: 0.55rem 0.85rem;
  background: rgba(29, 118, 226, 0.24);
  cursor: pointer;
  transition: 0.16s ease;
}

.management-panel__search button:hover {
  color: #fff;
  border-color: rgba(95, 212, 255, 0.68);
  box-shadow: 0 0 0.85rem rgba(47, 190, 255, 0.14);
}

.management-panel__player-row {
  display: grid;
  grid-template-columns: 1.25fr 0.45fr 0.95fr 0.55fr 0.65fr 2.15fr;
  gap: 1rem;
  align-items: center;
  margin-bottom: 0.72rem;
  padding: 1rem 1.15rem;
  border: 1px solid rgba(112, 201, 255, 0.16);
  background: rgba(4, 18, 36, 0.48);
  box-shadow: inset 0 0 1rem rgba(53, 149, 255, 0.05);
  transition: 0.16s ease;
}

.management-panel__player-row:hover {
  border-color: rgba(95, 212, 255, 0.38);
  background: rgba(8, 30, 58, 0.58);
}

.management-panel__player-row strong {
  display: block;
  margin-top: 0.25rem;
  color: #fff;
  font-size: 0.95rem;
  letter-spacing: 0.04em;
  text-transform: uppercase;
}

.management-panel__actions {
  display: flex;
  flex-wrap: wrap;
  gap: 0.4rem;
  justify-content: flex-end;
}

.management-panel__actions button {
  padding: 0.46rem 0.62rem;
  border: 1px solid rgba(112, 201, 255, 0.2);
  color: rgba(238, 247, 255, 0.82);
  background: rgba(14, 54, 92, 0.42);
  font: inherit;
  font-size: 0.72rem;
  font-weight: 900;
  letter-spacing: 0.08em;
  text-transform: uppercase;
  cursor: pointer;
  transition: 0.16s ease;
}

.management-panel__actions button:hover {
  color: #fff;
  border-color: rgba(95, 212, 255, 0.68);
  background: rgba(29, 118, 226, 0.32);
  box-shadow: 0 0 0.8rem rgba(47, 190, 255, 0.14);
}

.management-panel__punishment-layout {
  display: grid;
  grid-template-columns: 1fr 1.05fr 1.15fr;
  gap: 1rem;
  align-items: start;
}

.management-panel__punishment-panel {
  min-height: 34rem;
  padding: 1rem;
  border: 1px solid rgba(112, 201, 255, 0.16);
  background:
    linear-gradient(180deg, rgba(20, 61, 104, 0.34), rgba(4, 18, 36, 0.52)),
    repeating-linear-gradient(0deg, rgba(255,255,255,0.02) 0, rgba(255,255,255,0.02) 1px, transparent 1px, transparent 5px);
  box-shadow: inset 0 0 1rem rgba(53, 149, 255, 0.05);
}

.management-panel__punishment-panel--history {
  max-height: 55vh;
  overflow-y: auto;
}

.management-panel__target-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.75rem;
  width: 100%;
  margin-bottom: 0.55rem;
  padding: 0.82rem 0.9rem;
  border: 1px solid rgba(112, 201, 255, 0.14);
  color: rgba(238, 247, 255, 0.76);
  background: rgba(4, 18, 36, 0.44);
  font: inherit;
  text-align: left;
  cursor: pointer;
  transition: 0.16s ease;
}

.management-panel__target-row:hover,
.management-panel__target-row.active {
  color: #fff;
  border-color: rgba(95, 212, 255, 0.55);
  background: rgba(29, 118, 226, 0.22);
  box-shadow: inset 0.18rem 0 0 #34d8ff, 0 0 0.9rem rgba(47, 190, 255, 0.12);
}

.management-panel__target-row strong,
.management-panel__target-row small {
  display: block;
}

.management-panel__target-row strong {
  font-size: 0.95rem;
  text-transform: uppercase;
}

.management-panel__target-row small,
.management-panel__target-row em {
  color: rgba(238, 247, 255, 0.55);
  font-size: 0.72rem;
  font-style: normal;
  font-weight: 900;
  letter-spacing: 0.12em;
  text-transform: uppercase;
}

.management-panel__punishment-types {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 0.55rem;
  margin-bottom: 0.9rem;
}

.management-panel__punishment-types button {
  min-height: 4.2rem;
  padding: 0.75rem;
  border: 1px solid rgba(112, 201, 255, 0.16);
  color: rgba(238, 247, 255, 0.78);
  background: rgba(9, 31, 55, 0.5);
  font: inherit;
  text-align: left;
  cursor: pointer;
  transition: 0.16s ease;
}

.management-panel__punishment-types button:hover,
.management-panel__punishment-types button.active {
  color: #fff;
  border-color: rgba(255, 97, 97, 0.5);
  background: linear-gradient(135deg, rgba(255, 63, 72, 0.24), rgba(29, 118, 226, 0.14));
  box-shadow: 0 0 1rem rgba(255, 63, 72, 0.1);
}

.management-panel__punishment-types strong,
.management-panel__punishment-types small {
  display: block;
}

.management-panel__punishment-types strong {
  font-size: 0.85rem;
  letter-spacing: 0.1em;
  text-transform: uppercase;
}

.management-panel__punishment-types small {
  margin-top: 0.35rem;
  color: rgba(238, 247, 255, 0.5);
  font-size: 0.72rem;
  line-height: 1.25;
}

.management-panel__field {
  display: block;
  margin-bottom: 0.82rem;
}

.management-panel__field span {
  display: block;
  margin-bottom: 0.4rem;
  color: #4fe3ff;
  font-size: 0.74rem;
  font-weight: 900;
  letter-spacing: 0.18em;
  text-transform: uppercase;
}

.management-panel__field input,
.management-panel__field textarea {
  width: 100%;
  box-sizing: border-box;
  border: 1px solid rgba(112, 201, 255, 0.18);
  outline: 0;
  color: #fff;
  background: rgba(4, 18, 36, 0.58);
  font: inherit;
  letter-spacing: 0.04em;
}

.management-panel__field input {
  height: 2.7rem;
  padding: 0 0.8rem;
}

.management-panel__field textarea {
  min-height: 8rem;
  resize: none;
  padding: 0.85rem;
  line-height: 1.35;
}

.management-panel__field input:focus,
.management-panel__field textarea:focus {
  border-color: rgba(95, 212, 255, 0.62);
  box-shadow: 0 0 0.85rem rgba(47, 190, 255, 0.12);
}

.management-panel__execute {
  width: 100%;
  padding: 0.9rem 1rem;
  border: 1px solid rgba(255, 97, 97, 0.42);
  color: #fff;
  background: linear-gradient(90deg, rgba(255, 63, 72, 0.38), rgba(29, 118, 226, 0.2));
  font: inherit;
  font-size: 0.82rem;
  font-weight: 900;
  letter-spacing: 0.13em;
  text-transform: uppercase;
  cursor: pointer;
  transition: 0.16s ease;
}

.management-panel__execute:hover:not(:disabled) {
  border-color: rgba(255, 97, 97, 0.74);
  box-shadow: 0 0 1.1rem rgba(255, 63, 72, 0.16);
}

.management-panel__execute:disabled {
  cursor: not-allowed;
  opacity: 0.42;
}

.management-panel__history-list {
  display: grid;
  gap: 0.55rem;
}

.management-panel__history-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.85rem;
  padding: 0.82rem 0.9rem;
  border: 1px solid rgba(112, 201, 255, 0.12);
  background: rgba(4, 18, 36, 0.42);
}

.management-panel__history-row strong,
.management-panel__history-row span,
.management-panel__history-row small {
  display: block;
}

.management-panel__history-row strong {
  color: #fff;
  font-size: 0.82rem;
  line-height: 1.25;
  text-transform: uppercase;
}

.management-panel__history-row span,
.management-panel__history-row small,
.management-panel__history-note {
  color: rgba(238, 247, 255, 0.54);
  font-size: 0.72rem;
  letter-spacing: 0.08em;
  text-transform: uppercase;
}

.management-panel__history-row small {
  color: #4fe3ff;
  text-align: right;
}

.management-panel__history-note {
  margin: 0.85rem 0 0;
  line-height: 1.35;
}

.management-panel__error--compact {
  justify-content: center;
  margin-bottom: 0.75rem;
  padding: 0.75rem;
  font-size: 0.82rem;
}

.management-panel__modal {
  position: fixed;
  inset: 0;
  z-index: 30;
  display: grid;
  place-items: center;
  background: rgba(0, 5, 12, 0.72);
}

.management-panel__modal-card {
  width: min(34rem, 90vw);
  padding: 1.6rem;
  border: 1px solid rgba(255, 97, 97, 0.34);
  background: linear-gradient(180deg, rgba(13, 34, 58, 0.94), rgba(4, 12, 25, 0.96));
  box-shadow: 0 0 3rem rgba(255, 63, 72, 0.16), inset 0 0 2rem rgba(53, 149, 255, 0.08);
}

.management-panel__modal-card h3 {
  margin: 0.35rem 0 0.7rem;
  color: #fff;
  font-size: 1.5rem;
  text-transform: uppercase;
}

.management-panel__modal-card p {
  margin: 0 0 1rem;
  color: rgba(238, 247, 255, 0.62);
  line-height: 1.4;
}

.management-panel__modal-card dl {
  display: grid;
  gap: 0.65rem;
  margin: 0 0 1.25rem;
}

.management-panel__modal-card dl div {
  padding: 0.75rem;
  border: 1px solid rgba(112, 201, 255, 0.12);
  background: rgba(4, 18, 36, 0.42);
}

.management-panel__modal-card dt {
  color: #4fe3ff;
  font-size: 0.7rem;
  font-weight: 900;
  letter-spacing: 0.14em;
  text-transform: uppercase;
}

.management-panel__modal-card dd {
  margin: 0.25rem 0 0;
  color: #fff;
}

.management-panel__modal-actions {
  display: flex;
  justify-content: flex-end;
  gap: 0.75rem;
}

.management-panel__modal-actions button {
  padding: 0.75rem 1.25rem;
  border: 1px solid rgba(255, 255, 255, 0.24);
  color: #fff;
  background: rgba(255, 255, 255, 0.07);
  font: inherit;
  font-size: 0.78rem;
  font-weight: 900;
  letter-spacing: 0.12em;
  text-transform: uppercase;
  cursor: pointer;
}

.management-panel__modal-actions .is-danger {
  border-color: rgba(255, 97, 97, 0.62);
  background: rgba(255, 63, 72, 0.3);
}

.management-panel__database-layout {
  display: grid;
  grid-template-columns: 24rem minmax(0, 1fr);
  gap: 1rem;
  align-items: start;
}

.management-panel__database-panel {
  min-height: 34rem;
  padding: 1rem;
  border: 1px solid rgba(112, 201, 255, 0.16);
  background:
    linear-gradient(180deg, rgba(20, 61, 104, 0.34), rgba(4, 18, 36, 0.52)),
    repeating-linear-gradient(0deg, rgba(255,255,255,0.02) 0, rgba(255,255,255,0.02) 1px, transparent 1px, transparent 5px);
  box-shadow: inset 0 0 1rem rgba(53, 149, 255, 0.05);
}

.management-panel__database-panel--profile {
  max-height: 62vh;
  overflow-y: auto;
}

.management-panel__database-result {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.8rem;
  width: 100%;
  margin-bottom: 0.55rem;
  padding: 0.85rem 0.95rem;
  border: 1px solid rgba(112, 201, 255, 0.14);
  color: rgba(238, 247, 255, 0.78);
  background: rgba(4, 18, 36, 0.44);
  font: inherit;
  text-align: left;
  cursor: pointer;
  transition: 0.16s ease;
}

.management-panel__database-result:hover,
.management-panel__database-result.active {
  color: #fff;
  border-color: rgba(95, 212, 255, 0.55);
  background: rgba(29, 118, 226, 0.22);
  box-shadow: inset 0.18rem 0 0 #34d8ff, 0 0 0.9rem rgba(47, 190, 255, 0.12);
}

.management-panel__database-result strong,
.management-panel__database-result span,
.management-panel__database-result small,
.management-panel__database-result em {
  display: block;
}

.management-panel__database-result strong {
  font-size: 0.95rem;
  text-transform: uppercase;
}

.management-panel__database-result span,
.management-panel__database-result small,
.management-panel__database-result em {
  color: rgba(238, 247, 255, 0.54);
  font-size: 0.72rem;
  font-style: normal;
  font-weight: 900;
  letter-spacing: 0.1em;
  text-align: right;
  text-transform: uppercase;
}

.management-panel__database-result small {
  color: #4fe3ff;
}

.management-panel__profile-head {
  display: flex;
  align-items: flex-start;
  justify-content: space-between;
  gap: 1rem;
  margin-bottom: 1rem;
  padding: 1rem;
  border: 1px solid rgba(112, 201, 255, 0.16);
  background: rgba(4, 18, 36, 0.44);
}

.management-panel__profile-head h3 {
  margin: 0.25rem 0 0.35rem;
  color: #fff;
  font-size: 1.7rem;
  text-transform: uppercase;
}

.management-panel__profile-head p {
  margin: 0;
  color: rgba(238, 247, 255, 0.58);
  font-size: 0.85rem;
  letter-spacing: 0.1em;
  text-transform: uppercase;
}

.management-panel__profile-head > strong {
  color: #56ffd4;
  font-size: 0.8rem;
  letter-spacing: 0.14em;
  text-transform: uppercase;
}

.management-panel__profile-grid {
  display: grid;
  grid-template-columns: repeat(6, minmax(0, 1fr));
  gap: 0.65rem;
  margin-bottom: 1rem;
}

.management-panel__profile-grid article {
  padding: 0.8rem;
  border: 1px solid rgba(112, 201, 255, 0.13);
  background: rgba(4, 18, 36, 0.38);
}

.management-panel__profile-grid span,
.management-panel__detail-list span,
.management-panel__related-grid h4 {
  color: #4fe3ff;
  font-size: 0.7rem;
  font-weight: 900;
  letter-spacing: 0.15em;
  text-transform: uppercase;
}

.management-panel__profile-grid strong {
  display: block;
  margin-top: 0.35rem;
  color: #fff;
  font-size: 1.12rem;
}

.management-panel__related-grid {
  display: grid;
  grid-template-columns: repeat(2, minmax(0, 1fr));
  gap: 0.85rem;
  margin-bottom: 0.85rem;
}

.management-panel__related-grid--wide {
  grid-template-columns: repeat(2, minmax(0, 1fr));
}

.management-panel__related-grid section {
  padding: 1rem;
  border: 1px solid rgba(112, 201, 255, 0.14);
  background: rgba(4, 18, 36, 0.42);
}

.management-panel__related-grid h4 {
  margin: 0 0 0.75rem;
}

.management-panel__detail-list {
  display: grid;
  gap: 0.55rem;
}

.management-panel__detail-list span {
  display: flex;
  justify-content: space-between;
  gap: 0.8rem;
  color: rgba(238, 247, 255, 0.58);
}

.management-panel__detail-list strong {
  color: #fff;
  text-align: right;
}

.management-panel__related-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  gap: 0.8rem;
  margin-bottom: 0.45rem;
  padding: 0.68rem 0.75rem;
  border: 1px solid rgba(112, 201, 255, 0.1);
  background: rgba(9, 31, 55, 0.34);
}

.management-panel__related-row strong,
.management-panel__related-row span,
.management-panel__mini-empty,
.management-panel__active-ban {
  display: block;
  font-size: 0.74rem;
  letter-spacing: 0.08em;
  text-transform: uppercase;
}

.management-panel__related-row strong {
  color: #fff;
}

.management-panel__related-row span,
.management-panel__mini-empty {
  color: rgba(238, 247, 255, 0.54);
  text-align: right;
}

.management-panel__active-ban {
  margin-bottom: 0.55rem;
  padding: 0.7rem;
  border: 1px solid rgba(255, 97, 97, 0.28);
  color: #ffb0b0;
  background: rgba(76, 7, 12, 0.28);
}

.management-panel__staff-row strong {
  display: block;
  margin-top: 0.25rem;
  color: #fff;
  font-size: 0.95rem;
  letter-spacing: 0.04em;
  text-transform: uppercase;
}

.management-panel__staff-id span {
  display: block;
  margin-top: 0.2rem;
  color: rgba(238, 247, 255, 0.5);
  font-size: 0.78rem;
  letter-spacing: 0.1em;
}

.management-panel__staff-row .is-online {
  color: #56ffd4;
  text-shadow: 0 0 0.8rem rgba(86, 255, 212, 0.45);
}

.management-panel__staff-row .is-warn {
  color: #ffd36a;
  text-shadow: 0 0 0.8rem rgba(255, 211, 106, 0.35);
}

.management-panel__staff-row .is-offline {
  color: rgba(238, 247, 255, 0.48);
}

.management-panel__notice {
  margin-top: 1rem;
  justify-content: flex-start;
  gap: 0.9rem;
  color: rgba(238, 247, 255, 0.72);
}

.management-panel__notice strong {
  color: #fff;
  text-transform: uppercase;
}

.management-panel__notes {
  display: grid;
  gap: 0.72rem;
  margin-top: 1rem;
}

.management-panel__notes div {
  display: flex;
  align-items: center;
  gap: 0.75rem;
  padding: 0.9rem 1rem;
  border: 1px solid rgba(112, 201, 255, 0.12);
  color: rgba(238, 247, 255, 0.72);
  background: rgba(4, 18, 36, 0.36);
}
</style>
