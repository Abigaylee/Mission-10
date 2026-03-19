import { useEffect, useMemo, useState, type CSSProperties } from 'react';

type Team = {
  teamName?: string;
};

type Bowler = {
  bowlerId?: number;
  bowlerID?: number;
  bowlerFirstName?: string | null;
  bowlerMiddleInit?: string | null;
  bowlerLastName?: string | null;
  bowlerAddress?: string | null;
  bowlerCity?: string | null;
  bowlerState?: string | null;
  bowlerZip?: string | null;
  bowlerPhoneNumber?: string | null;
  team?: Team | null;
};

function BowlerFullName({ b }: { b: Bowler }) {
  return [
    b.bowlerFirstName,
    b.bowlerMiddleInit,
    b.bowlerLastName,
  ]
    .filter((x): x is string => typeof x === 'string' && x.trim().length > 0)
    .join(' ');
}

function BowlersTable() {
  const [bowlerData, setBowlerData] = useState<Bowler[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);

  useEffect(() => {
    const fetchBowlers = async () => {
      setLoading(true);
      setError(null);

      try {
        const rsp = await fetch('/api/Bowler');
        if (!rsp.ok) {
          throw new Error(`Request failed with status ${rsp.status}`);
        }

        const b = (await rsp.json()) as Bowler[];
        setBowlerData(b);
      } catch (e) {
        setError(e instanceof Error ? e.message : 'Unknown error');
      } finally {
        setLoading(false);
      }
    };

    fetchBowlers();
  }, []);

  const filteredBowlers = useMemo(() => {
    return bowlerData.filter((b) => {
      const name = b.team?.teamName ?? '';
      return name === 'Marlins' || name === 'Sharks';
    });
  }, [bowlerData]);

  const tableStyle: CSSProperties = {
    width: '100%',
    borderCollapse: 'collapse',
  };

  const cellStyle: CSSProperties = {
    border: '1px solid #ccc',
    padding: '6px 8px',
    verticalAlign: 'top',
  };

  return (
    <div>
      {loading && <p>Loading bowlers...</p>}
      {error && <p style={{ color: 'red' }}>{error}</p>}

      {!loading && !error && (
        <table style={tableStyle}>
          <thead>
            <tr>
              <th style={cellStyle}>Bowler Name</th>
              <th style={cellStyle}>Team Name</th>
              <th style={cellStyle}>Address</th>
              <th style={cellStyle}>City</th>
              <th style={cellStyle}>State</th>
              <th style={cellStyle}>Zip</th>
              <th style={cellStyle}>Phone Number</th>
            </tr>
          </thead>
          <tbody>
            {filteredBowlers.map((b) => {
              const key = b.bowlerId ?? b.bowlerID ?? `${b.bowlerFirstName}-${b.bowlerLastName}`;

              return (
                <tr key={key}>
                  <td style={cellStyle}>
                    <BowlerFullName b={b} />
                  </td>
                  <td style={cellStyle}>{b.team?.teamName ?? ''}</td>
                  <td style={cellStyle}>{b.bowlerAddress ?? ''}</td>
                  <td style={cellStyle}>{b.bowlerCity ?? ''}</td>
                  <td style={cellStyle}>{b.bowlerState ?? ''}</td>
                  <td style={cellStyle}>{b.bowlerZip ?? ''}</td>
                  <td style={cellStyle}>{b.bowlerPhoneNumber ?? ''}</td>
                </tr>
              );
            })}
          </tbody>
        </table>
      )}
    </div>
  );
}

export default BowlersTable;

